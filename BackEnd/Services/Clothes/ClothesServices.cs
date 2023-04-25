using AutoMapper;
using ClothesWeb.Models;
using ClothesWeb.Repository.Account;
using ClothesWeb.Repository.Clothes;
using Microsoft.Extensions.Configuration;

namespace ClothesWeb.Services.Clothes
{
    public class ClothesServices:IClothesServices
    {
        private readonly IMapper _mapper;
        private readonly IClothesRespository _clothesRespository;
        public ClothesServices(IClothesRespository clothesRespository, IMapper mapper)
        {
            _clothesRespository = clothesRespository;
            _mapper = mapper;
        }

        public async Task<string> CreatePost(ClothesDB clothesInfo)
        {
            string result;
            var temp = await _clothesRespository.GetPost(clothesInfo);
            if (temp != null)
            {
                result = await _clothesRespository.CreatePost(clothesInfo);
                return result;
            }
            return "Clothes already exits";
        }
    }
}
