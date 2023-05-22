using AutoMapper;
using ClothesWeb.Models;
using ClothesWeb.Repository.Order;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using ClothesWeb.Dto.Order;
using ClothesWeb.Services.Account;
using ClothesWeb.Repository.Account;

namespace ClothesWeb.Services.Order
{
    public class OrderServices:IOrderServices
    {
        private readonly IOrderRespository _orderRespository;
        private readonly IAccountRespository _accountRespository;
        public OrderServices(IOrderRespository orderRespository, IMapper mapper, IAccountRespository accountRespository)
        {
            _orderRespository = orderRespository;
            _accountRespository = accountRespository;
        }
        public async Task<string> CreateOrder(OrderDB orderInfo)
        {
            if (await _orderRespository.IsOrderExtis(orderInfo))
            {
                return await _orderRespository.UpdateOrderQuantity(orderInfo);
            }
            else
            {
                return await _orderRespository.CreateOrderDB(orderInfo);
            }  
        }

        public async Task<List<OrderDB>> GetAllOrdersById(int accountId)
        {
            return await _orderRespository.GetAllOrderById(accountId);
        }

        public void sendMail(string userEmail, List<string> clothesName, string sumMoney)
        {
            var email = new MimeMessage();
            var body = "<h1>Thông tin đơn hàng</h1><br/>";
            foreach (string i in clothesName) 
            {
                body += "<div> -" + i + "</div><br/>";
            }
            body += "<h1>" + sumMoney + "</h1>";
            email.From.Add(new MailboxAddress("haoquy1", "haoquy1@gmail.com"));
            email.To.Add(new MailboxAddress(userEmail.Split("@")[0], userEmail));

            email.Subject = "Hóa đơn mua hàng";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body

            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 465, true);
                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("haoquy1@gmail.com", "jnzrfmfjcnprphjt");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
        public async Task<bool> UpdateStatusAndSendEmail(PayObject pay, int accountId)
        {
            var email = await _accountRespository.GetEmail(accountId);
            sendMail(email, pay.ListClothesName, pay.sum);
            return await _orderRespository.UpdateOrderStatus(pay.ListOrder);
        }
    }
}
