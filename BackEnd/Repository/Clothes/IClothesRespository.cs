using ClothesWeb.Models;

namespace ClothesWeb.Repository.Clothes
{
    public interface IClothesRespository
    {
        public Task<ClothesDB> GetPost(ClothesDB clothesDB);
        public Task<string> CreatePost(ClothesDB clothesDB);
        public Task<string> UpdatePost(ClothesDB clothesDB);
        public Task<string> DeletePost(ClothesDB clothesDB);
    }
}
