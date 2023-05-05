using AutoMapper;
using ClothesWeb.Dto.Account;
using ClothesWeb.Models;
using ClothesWeb.Repository.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<string> CreateAccount(CreateAccountDto accountCreateInfo)
        {
            string result;
            var account = _mapper.Map<AccountDB>(accountCreateInfo);
            var temp = await _accountRespository.GetAccount(account);
            if (temp.id == 0)
            {
                CreatePasswordHash(accountCreateInfo.Password, out byte[] passwordHash, out byte[] passwordSalt);
                result = await _accountRespository.CreateAccountDB(account, passwordHash, passwordSalt);
                return result;
            }
            return "Account already exits";
        }
        public async Task<GetAccountDto> Login(LoginAccountDto accountLoginInfo)
        {
            GetAccountDto accountInfo = new GetAccountDto();
            var account = _mapper.Map<AccountDB>(accountLoginInfo);
            var temp = await _accountRespository.GetAccount(account);
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
