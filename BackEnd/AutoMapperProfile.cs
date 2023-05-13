using AutoMapper;
using ClothesWeb.Dto.Account;
using ClothesWeb.Dto.Clothes;
using ClothesWeb.Models;
using System.Data;
using System.Data.Common;

namespace ClothesWeb
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Account Mapping
            CreateMap<CreateAccountDto, AccountDB>();
            CreateMap<LoginAccountDto, AccountDB>();
            CreateMap<AccountDB, GetAccountDto>();
            CreateMap<IDataRecord, AccountDB>();
            //Clothes Mapping
            CreateMap<IDataRecord, ClothesDB>();
            CreateMap<ClothesDB, ClothesHalfInfo>();
            CreateMap<ClothesDB, ClothesCart>();
            //Order Mapping
            CreateMap<IDataRecord, OrderDB>();
        }
    }
}
