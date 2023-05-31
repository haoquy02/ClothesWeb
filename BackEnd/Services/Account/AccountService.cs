using AutoMapper;
using ClothesWeb.Dto.Account;
using ClothesWeb.Models;
using ClothesWeb.Repository.Account;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClothesWeb.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAccountRespository _accountRespository;
        public AccountService(IAccountRespository accountRespository, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _accountRespository = accountRespository;
            _mapper = mapper;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public void SendVerification(string userEmail, string Code)
        {
            var email = new MimeMessage();
            var body = "<h1>Mã xác nhận</h1><br/>" + "<div>" + Code + "</div>";
            email.From.Add(new MailboxAddress("haoquy1", "haoquy1@gmail.com"));
            email.To.Add(new MailboxAddress(userEmail.Split("@")[0], userEmail));
            email.Subject = "Mã xác nhận tài khoản";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body

            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 465, true);
                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("haoquy1@gmail.com", "jnzrfmfjcnprphjt");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
        public string CreateToken(AccountDB user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            var appSettingsToken = _configuration.GetSection("AppSetting:Token").Value;
            if (appSettingsToken is null)
            {
                throw new Exception("AppSetting Token is null!");
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> CreateAccount(CreateAccountDto accountCreateInfo, string Code)
        {
            var account = _mapper.Map<AccountDB>(accountCreateInfo);
            if (accountCreateInfo.VerificationCodeFromUser == Code)
            {
                CreatePasswordHash(accountCreateInfo.Password, out byte[] passwordHash, out byte[] passwordSalt);
                return await _accountRespository.CreateAccountDB(account, passwordHash, passwordSalt); ;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> VerifyAccount(string userName)
        {
            var temp = await _accountRespository.GetAccount(userName);
            if (temp.id == 0)
            {
                return true;
            }
            return false; ;
        }
        public async Task<GetAccountDto> Login(LoginAccountDto accountLoginInfo)
        {
            GetAccountDto accountInfo = new GetAccountDto();
            var account = _mapper.Map<AccountDB>(accountLoginInfo);
            var temp = await _accountRespository.GetAccount(account.Username);
            if (temp.id != 0)
            {
                if (VerifyPasswordHash(accountLoginInfo.Password, temp.PasswordHash, temp.PasswordSalt))
                {
                    var token = CreateToken(temp);
                    accountInfo = _mapper.Map<GetAccountDto>(temp);
                    accountInfo.Token = token;
                    accountInfo.Status = "Login successful";
                    return accountInfo;
                }
                accountInfo.Status = "Password not correct";
                return accountInfo;
            }
            accountInfo.Status = "Username not found";
            return accountInfo;
        }
    }
}
