using AutoMapper;
using ClothesWeb.Models;
using ClothesWeb.Repository.Clothes;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace ClothesWeb.Repository.Order
{
    public class OrderRespository : IOrderRespository
    {
        private readonly ClothesDBContext _context;
        private readonly IClothesRespository _clothesRespository;
        private readonly IMapper _mapper;
        public OrderRespository(ClothesDBContext context, IMapper mapper, IClothesRespository clothesRespository)
        {
            _context = context;
            _mapper = mapper;
            _clothesRespository = clothesRespository;
        }
        public async Task<string> CreateOrderDB(OrderDB orderCreateInfo)
        {
            var updateResult = await _clothesRespository.UpdateQuantity(orderCreateInfo.Quantity, orderCreateInfo.ClothesId);
            if (updateResult)
            {
                var connection = _context.GetDbConnection();
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = (SqlConnection)connection;
                    command.CommandText = "INSERT INTO OrderClothes (AccountId, ClothesId, Quantity, Size, Color, Status ) " +
                        "VALUES (@AccountId,@ClothesId,@Quantity,@Size,N'None',@Status)";
                    command.Parameters.AddWithValue("@AccountId", orderCreateInfo.AccountId);
                    command.Parameters.AddWithValue("@ClothesId", orderCreateInfo.ClothesId);
                    command.Parameters.AddWithValue("@Quantity", orderCreateInfo.Quantity);
                    command.Parameters.AddWithValue("@Size", orderCreateInfo.Size);
                    command.Parameters.AddWithValue("@Status", orderCreateInfo.Status);
                    await command.ExecuteScalarAsync();
                }
                connection.Close();
                return "Add to cart successful";
            }
            else
            {
                return "Failed";
            }
        }
        public async Task<List<OrderDB>> GetAllOrderById(int accountId)
        {
            var connection = _context.GetDbConnection();
            connection.Open();
            List<OrderDB> listOrder = new();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select * From OrderClothes " +
                    "INNER Join OrderStatus "+
                    "On [OrderClothes].StatusId = [OrderStatus].id " +
                    "Where AccountId = " + accountId+ " and StatusId = 1";
                var reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    listOrder.Add(_mapper.Map<IDataReader, OrderDB>(reader));
                }
            }
            connection.Close();
            return listOrder;
        }

        public async Task<bool> IsOrderExtis(OrderDB orderCreateInfo)
        {
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = (SqlConnection)connection;
                command.CommandText = "Select * From OrderClothes Where AccountId = @AccountId and ClothesId = @ClothesId and Size = @Size";
                command.Parameters.AddWithValue("@AccountId", orderCreateInfo.AccountId);
                command.Parameters.AddWithValue("@ClothesId", orderCreateInfo.ClothesId);
                command.Parameters.AddWithValue("@Size", orderCreateInfo.Size);
                var reader = command.ExecuteReader();
                await reader.ReadAsync();
                if (reader.HasRows)
                {
                    connection.Close();
                    return true;
                }
            }
            connection.Close();
            return false;
        }

        public async Task<bool> UpdateOrderStatus(List<int> orderId)
        {
            var connection = _context.GetDbConnection();
            connection.Open();
            foreach (int id in orderId)
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = (SqlConnection)connection;
                    command.CommandText =
                     "Update OrderClothes SET StatusId= N'2' WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteScalarAsync();
                }
            }
            connection.Close();
            return true;
        }
        public async Task<string> UpdateOrderQuantity(OrderDB orderCreateInfo)
        {
            var oldQuantity = 0;
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = (SqlConnection)connection;
                command.CommandText = "Select Quantity From OrderClothes Where AccountId = @AccountId and ClothesId = @ClothesId and Size = @Size";
                command.Parameters.AddWithValue("@AccountId", orderCreateInfo.AccountId);
                command.Parameters.AddWithValue("@ClothesId", orderCreateInfo.ClothesId);
                command.Parameters.AddWithValue("@Size", orderCreateInfo.Size);
                var reader = command.ExecuteReader();
                await reader.ReadAsync();
                if (reader.HasRows)
                {
                    oldQuantity = (int)reader["Quantity"];
                }
            }
            connection.Close();
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = (SqlConnection)connection;
                command.CommandText =
                 "Update OrderClothes SET Quantity= @Quantity WHERE AccountId = @AccountId and ClothesId = @ClothesId and Size = @Size";
                command.Parameters.AddWithValue("@Quantity", orderCreateInfo.Quantity + oldQuantity);
                command.Parameters.AddWithValue("@AccountId", orderCreateInfo.AccountId);
                command.Parameters.AddWithValue("@ClothesId", orderCreateInfo.ClothesId);
                command.Parameters.AddWithValue("@Size", orderCreateInfo.Size);
                await command.ExecuteScalarAsync();
            }
            connection.Close();
            return "Add to cart successful";
        }
    }
}
