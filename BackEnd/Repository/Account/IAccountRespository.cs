using ClothesWeb.Models;

namespace ClothesWeb.Repository.Account
{
    public interface IAccountRespository
    {
        public Task<string> CreateAccountDB(AccountDB accountCreateInfo, byte[] passwordHash, byte[] passwordSalt);
        public Task<AccountDB> GetAccount(AccountDB accountInfo);
        public Task<string> GetEmail(int accountId);
    }
}
