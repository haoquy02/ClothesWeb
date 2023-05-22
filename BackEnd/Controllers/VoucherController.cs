using ClothesWeb.Dto.Voucher;
using ClothesWeb.Models;
using ClothesWeb.Repository.Voucher;
using ClothesWeb.Services.Order;
using ClothesWeb.Services.Voucher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherServices _voucherServices;
        public VoucherController(IVoucherServices voucherServices)
        {
            _voucherServices = voucherServices;
        }
        [HttpGet]
        public async Task<ActionResult<VoucherDB>> GetVoucher(string code)
        {
            var accountId = int.Parse(Request.Cookies["id"]);
            var voucher=  await _voucherServices.GetVoucher(code);
            voucher.HasUsed = _voucherServices.CheckVoucherInAccountr(voucher.id, accountId);
            return Ok(voucher);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddVoucherToAccount(int voucherId)
        {
            var accountId = int.Parse(Request.Cookies["id"]);
            return await _voucherServices.ApplyVoucher(voucherId, accountId);
        }
    }
}
