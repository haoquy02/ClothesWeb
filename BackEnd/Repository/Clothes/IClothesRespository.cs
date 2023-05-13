using ClothesWeb.Dto.Clothes;
using ClothesWeb.Models;

namespace ClothesWeb.Repository.Clothes
{
    public interface IClothesRespository
    {
        public Task<ClothesDB> GetClothes(int clothesID);
        public Task<string> CreateClothes(ClothesDB clothesDB);
        public Task<string> UpdateClothes(ClothesDB clothesDB);
        public Task<string> DeleteClothes(ClothesDB clothesDB);
        public Task<List<ClothesDB>> GetAllClothes();
        public Task<bool> UpdateQuantity(int quantity, int id);
        public Task<ClothesDB> GetClothesCartById(int id);
    }
}
