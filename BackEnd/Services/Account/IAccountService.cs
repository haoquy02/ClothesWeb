using ClothesWeb.Dto.Account;
using ClothesWeb.Models;

namespace ClothesWeb.Services.Account
{
    public interface IAccountService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public string CreateToken(AccountDB user);
        public Task<bool> CreateAccount(CreateAccountDto account, string Code);
        public Task<bool> VerifyAccount(string userName);
        public Task<GetAccountDto> Login(LoginAccountDto account);
        public void SendVerification(string userEmail, string code);
    }
}
