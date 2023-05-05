using ClothesWeb.Dto.Clothes;
using ClothesWeb.Models;

namespace ClothesWeb.Repository.Clothes
{
    public interface IClothesRespository
    {
        public Task<ClothesDB> GetPost(int clothesID);
        public Task<string> CreatePost(ClothesDB clothesDB);
        public Task<string> UpdatePost(ClothesDB clothesDB);
        public Task<string> DeletePost(ClothesDB clothesDB);
        public Task<List<ClothesDB>> GetAllPost();
    }
}
