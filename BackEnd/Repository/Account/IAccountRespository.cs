using ClothesWeb.Models;

namespace ClothesWeb.Repository.Account
{
    public interface IAccountRespository
    {
        public Task<bool> CreateAccountDB(AccountDB accountCreateInfo, byte[] passwordHash, byte[] passwordSalt);
        public Task<AccountDB> GetAccount(string Username);
        public Task<string> GetEmail(int accountId);
    }
}
