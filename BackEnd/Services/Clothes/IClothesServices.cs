using ClothesWeb.Dto.Account;
using ClothesWeb.Models;

namespace ClothesWeb.Services.Clothes
{
    public interface IClothesServices
    {
        public Task<string> CreatePost(ClothesDB clothesInfo);
    }
}
