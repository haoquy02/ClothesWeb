using ClothesWeb.Dto.Voucher;
using ClothesWeb.Models;

namespace ClothesWeb.Repository.Voucher
{
    public interface IVoucherRepository
    {
        public Task<VoucherDB> GetVoucher(string voucher);
        public Task<bool> ApplyVoucher(int voucherId, int accountId);
        public Task<bool> UpdateAmount(int voucherId);
        public int GetAmount(int voucherId);
        public bool CheckVoucherInAccountr(int voucherId, int accountId);
    }
}
