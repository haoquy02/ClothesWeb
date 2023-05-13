using AutoMapper;
using ClothesWeb.Models;
using ClothesWeb.Repository.Clothes;
using Microsoft.Data.SqlClient;
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
                return "Add to cart Successful";
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
                command.CommandText = "Select * From OrderClothes Where AccountId = " + accountId+ "and Status = N'Trong giỏ hàng'";
                var reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    listOrder.Add(_mapper.Map<IDataReader, OrderDB>(reader));
                }
            }
            connection.Close();
            return listOrder;
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
                     "Update OrderClothes SET Status= N'Đã thanh toán' WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteScalarAsync();
                }
            }
            connection.Close();
            return true;
        }
    }
}
