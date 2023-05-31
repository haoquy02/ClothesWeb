using AutoMapper;
using ClothesWeb.Dto.Voucher;
using ClothesWeb.Models;
using ClothesWeb.Repository.Clothes;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClothesWeb.Repository.Voucher
{
    public class VoucherRespository : IVoucherRepository
    {
        private readonly ClothesDBContext _context;
        private readonly IMapper _mapper;
        public VoucherRespository(ClothesDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VoucherDB> GetVoucher(string voucherCode)
        {
            var connection = _context.GetDbConnection();
            VoucherDB voucherDB = new VoucherDB();
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = (SqlConnection)connection;
                command.CommandText = "Select * From Voucher Where code = @Code";
                command.Parameters.AddWithValue("@Code", voucherCode);
                var reader = command.ExecuteReader();
                await reader.ReadAsync();
                if (reader.HasRows)
                {
                    voucherDB = _mapper.Map<IDataReader, VoucherDB>(reader);
                }
            }
            connection.Close();
            return voucherDB;
        }
        public async Task<bool> ApplyVoucher(int voucherId, int accountId)
        {
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = (SqlConnection)connection;
                command.CommandText = "INSERT INTO AccountVoucher (AccountId, VoucherId) " +
                    "VALUES (@AccountId,@VoucherId)";
                command.Parameters.AddWithValue("@AccountId", accountId);
                command.Parameters.AddWithValue("@VoucherId", voucherId);
                await command.ExecuteScalarAsync();
            }
            connection.Close();
            return true;
        }
        public int GetAmount(int voucherId)
        {
            int amount = 0;
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select Amount From Voucher Where id = " + voucherId + "";
                var reader = command.ExecuteReader();
                reader.ReadAsync();
                if (reader.HasRows)
                {
                    amount = (int)reader["Amount"];
                }
            }
            connection.Close();
            return amount;
        }
        public async Task<bool> UpdateAmount(int voucherId)
        {
            var amount = GetAmount(voucherId);
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = (SqlConnection)connection;
                command.CommandText =
                     "Update Voucher SET Amount= @Amount WHERE id = @id";
                command.Parameters.AddWithValue("@Amount", amount--);
                command.Parameters.AddWithValue("@id", voucherId);
                await command.ExecuteScalarAsync(); ;
            }
            connection.Close();
            return true;
        }

        public bool CheckVoucherInAccountr(int voucherId, int accountId)
        {
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select * From AccountVoucher Where AccountId =" + accountId + " and VoucherId ="+voucherId;
                var reader = command.ExecuteReader();
                reader.ReadAsync();
                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
            }
            connection.Close();
            return false;
        }
    }
}
