using AutoMapper;
using ClothesWeb.Models;
using ClothesWeb.Repository.Clothes;
using ClothesWeb.Repository.Order;
using Microsoft.Identity.Client;

namespace ClothesWeb.Services.Order
{
    public class OrderServices:IOrderServices
    {
        private readonly IMapper _mapper;
        private readonly IOrderRespository _orderRespository;
        public OrderServices(IOrderRespository orderRespository, IMapper mapper)
        {
            _orderRespository = orderRespository;
            _mapper = mapper;
        }
        public async Task<string> CreateOrder(OrderDB orderInfo)
        {
            return await _orderRespository.CreateOrderDB(orderInfo);
        }

        public async Task<List<OrderDB>> GetAllOrdersById(int accountId)
        {
            return await _orderRespository.GetAllOrderById(accountId);
        }

        public async Task<bool> UpdateStatus(List<int> orderId)
        {
            return await _orderRespository.UpdateOrderStatus(orderId);
        }
    }
}
