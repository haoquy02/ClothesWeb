using ClothesWeb.Models;

namespace ClothesWeb.Services.Order
{
    public interface IOrderServices
    {
        public Task<string> CreateOrder(OrderDB orderInfo);
        public Task<List<OrderDB>> GetAllOrdersById(int accountId);
        public Task<bool> UpdateStatus(List<int> orderId);
    }
}
