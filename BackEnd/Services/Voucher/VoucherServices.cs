using AutoMapper;
using ClothesWeb.Dto.Voucher;
using ClothesWeb.Models;
using ClothesWeb.Repository.Voucher;

namespace ClothesWeb.Services.Voucher
{
    public class VoucherServices:IVoucherServices
    {
        private readonly IMapper _mapper;
        private readonly IVoucherRepository _voucherRespository;
        public VoucherServices(IVoucherRepository voucherRespository, IMapper mapper)
        {
            _voucherRespository = voucherRespository;
            _mapper = mapper;
        }

        public async Task<bool> ApplyVoucher(int voucherId, int accountId)
        {
            await _voucherRespository.UpdateAmount(voucherId);
            return await _voucherRespository.ApplyVoucher(voucherId, accountId);
        }

        public async Task<VoucherDB> GetVoucher(string voucherCode)
        {
            return await _voucherRespository.GetVoucher(voucherCode);
        }
        public bool CheckVoucherInAccountr(int voucherId, int accountId)
        {
            return  _voucherRespository.CheckVoucherInAccountr(voucherId,accountId);
        }
    }
}
