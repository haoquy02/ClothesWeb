using ClothesWeb.Models;

namespace ClothesWeb.Repository.Order
{
    public interface IOrderRespository
    {
        public Task<string> CreateOrderDB(OrderDB accountCreateInfo);
        public Task<List<OrderDB>> GetAllOrderById(int accountId);
        public Task<bool> UpdateOrderStatus(List<int> orderId);
    }
}
