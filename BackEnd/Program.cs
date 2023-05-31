using AutoMapper.Data;
using ClothesWeb.Models;
using ClothesWeb.Repository.Account;
using ClothesWeb.Repository.Clothes;
using ClothesWeb.Repository.Order;
using ClothesWeb.Repository.Voucher;
using ClothesWeb.Services.Account;
using ClothesWeb.Services.Clothes;
using ClothesWeb.Services.Order;
using ClothesWeb.Services.Voucher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ClothesDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(),typeof(Program).Assembly);
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRespository, AccountRespository>();
builder.Services.AddScoped<IClothesServices, ClothesServices>();
builder.Services.AddScoped<IClothesRespository, ClothesRespository>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderRespository, OrderRespository>();
builder.Services.AddScoped<IVoucherServices, VoucherServices>();
builder.Services.AddScoped<IVoucherRepository, VoucherRespository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddCookie(x =>
{
    x.Cookie.Name = "token";
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
        .GetBytes(builder.Configuration.GetSection("AppSetting:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["token"];
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();
app.UseCors(options => options.WithOrigins("http://localhost:3000")
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials());
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
