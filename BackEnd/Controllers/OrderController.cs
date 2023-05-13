using Azure.Core;
using ClothesWeb.Models;
using ClothesWeb.Services.Account;
using ClothesWeb.Services.Order;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices  _orderService;
        public OrderController(IOrderServices orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> PostCreatNewOrder(OrderDB orderCreateInfo)
        {
            string result;
            if (orderCreateInfo == null)
            {
                return BadRequest();
            }
            else
            {
                result = await _orderService.CreateOrder(orderCreateInfo);
                return Ok(result);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<OrderDB>>> GetAllOrder()
        {
            var accountId = int.Parse(Request.Cookies["id"]);
            return await _orderService.GetAllOrdersById(accountId);
        }
        [HttpPut("OrderId")]
        public async Task<ActionResult<bool>> UpdateOrderStatus(List<int> OrderId)
        {
            return await _orderService.UpdateStatus(OrderId);
        }
    }
}
