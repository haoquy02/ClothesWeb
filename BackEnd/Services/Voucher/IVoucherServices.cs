using ClothesWeb.Dto.Voucher;
using ClothesWeb.Models;

namespace ClothesWeb.Services.Voucher
{
    public interface IVoucherServices
    {
        public Task<VoucherDB> GetVoucher(string voucher);
        public Task<bool> ApplyVoucher(int voucherId, int accountId);
        public bool CheckVoucherInAccountr(int voucherId, int accountId);
    }
}
