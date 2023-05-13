using AutoMapper;
using ClothesWeb.Dto.Clothes;
using ClothesWeb.Models;
using ClothesWeb.Repository.Account;
using ClothesWeb.Repository.Clothes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;

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

        public async Task<string> CreateClothes(ClothesDB clothesInfo)
        {
            string result;
            var temp = await _clothesRespository.GetClothes(clothesInfo.id);
            if (temp != null)
            {
                result = await _clothesRespository.CreateClothes(clothesInfo);
                return result;
            }
            return "Clothes already exits";
        }
        public async Task<List<ClothesHalfInfo>> GetAllClothes()
        {
            var result = await _clothesRespository.GetAllClothes();
            List<ClothesHalfInfo> listClothesInfo = new();
            foreach (var clothesInfo in result)
            {
                listClothesInfo.Add(_mapper.Map<ClothesHalfInfo>(clothesInfo));
            }
            return listClothesInfo;
        }
        public async Task<ClothesDB> GetClothesById(int ClothesId)
        {
            return await _clothesRespository.GetClothes(ClothesId);
        }
        public async Task<ClothesCart> GetClothesCartById(int ClothesId)
        {
            var clothesDB = await _clothesRespository.GetClothesCartById(ClothesId);
            return _mapper.Map<ClothesCart>(clothesDB);
        }
    }
}
