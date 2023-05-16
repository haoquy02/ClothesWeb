using ClothesWeb.Models;

namespace ClothesWeb.Repository.Order
{
    public interface IOrderRespository
    {
        public Task<string> CreateOrderDB(OrderDB orderCreateInfo);
        public Task<List<OrderDB>> GetAllOrderById(int accountId);
        public Task<bool> UpdateOrderStatus(List<int> orderId);
        public Task<bool> IsOrderExtis(OrderDB orderCreateInfo);
        public Task<string> UpdateOrderQuantity(OrderDB orderCreateInfo);
    }
}
