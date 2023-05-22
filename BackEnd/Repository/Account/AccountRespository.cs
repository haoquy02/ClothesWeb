using AutoMapper;
using ClothesWeb.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClothesWeb.Repository.Account
{
    public class AccountRespository : IAccountRespository
    {
        private readonly ClothesDBContext _context;
        private readonly IMapper _mapper;
        public AccountRespository(ClothesDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccountDB> GetAccount(AccountDB accountInfo)
        {
            var connection = _context.GetDbConnection();
            AccountDB account = new AccountDB();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select * From Account Where Username = '" + accountInfo.Username + "'";
                var reader = command.ExecuteReader();
                await reader.ReadAsync();
                if (reader.HasRows)
                {
                    account = _mapper.Map<AccountDB>(reader);
                }
            }
            connection.Close();
            return account;
        }

        public async Task<string> CreateAccountDB(AccountDB accountCreateInfo, byte[] passwordHash, byte[] passwordSalt)
        {
            accountCreateInfo.PasswordHash = passwordHash;
            accountCreateInfo.PasswordSalt = passwordSalt;
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = (SqlConnection)connection;
                command.CommandText = "Insert Into Account (Username, PasswordHash, PasswordSalt, Email,Avatar, Role ) " +
                    "VALUES (@Username,@PasswordHash,@PasswordSalt,@Email,N'None',N'User')";
                command.Parameters.AddWithValue("@Username", accountCreateInfo.Username);
                command.Parameters.AddWithValue("@PasswordHash", accountCreateInfo.PasswordHash);
                command.Parameters.AddWithValue("@PasswordSalt", accountCreateInfo.PasswordSalt);
                command.Parameters.AddWithValue("@Email", accountCreateInfo.Email);
                await command.ExecuteScalarAsync();
            }
            connection.Close();
            return "Create Account Successful";
        }
        public async Task<string> GetEmail(int accountd)
        {
            var connection = _context.GetDbConnection();
            string email = string.Empty;
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select email From Account Where id = '" + accountd + "'";
                var reader = command.ExecuteReader();
                await reader.ReadAsync();
                if (reader.HasRows)
                {
                    email = reader["Email"].ToString();
                }
            }
            connection.Close();
            return email;
        }
    }
}
