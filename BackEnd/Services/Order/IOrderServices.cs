using ClothesWeb.Dto.Order;
using ClothesWeb.Models;

namespace ClothesWeb.Services.Order
{
    public interface IOrderServices
    {
        public Task<string> CreateOrder(OrderDB orderInfo);
        public Task<List<OrderDB>> GetAllOrdersById(int accountId);
        public Task<bool> UpdateStatusAndSendEmail(PayObject pay, int accountId);
    }
}
