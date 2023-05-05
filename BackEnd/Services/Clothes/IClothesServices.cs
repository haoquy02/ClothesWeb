using ClothesWeb.Dto.Account;
using ClothesWeb.Dto.Clothes;
using ClothesWeb.Models;

namespace ClothesWeb.Services.Clothes
{
    public interface IClothesServices
    {
        public Task<string> CreatePost(ClothesDB clothesInfo);
        public Task<List<ClothesHalfInfo>> GetAllClothes();
        public Task<ClothesDB> GetClothesByName(int ClothesId);
    }
}
