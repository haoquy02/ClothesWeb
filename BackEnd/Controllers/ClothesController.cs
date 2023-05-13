using ClothesWeb.Dto.Account;
using ClothesWeb.Dto.Clothes;
using ClothesWeb.Models;
using ClothesWeb.Services.Account;
using ClothesWeb.Services.Clothes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        private readonly IClothesServices _clothesServices;
        public ClothesController(IClothesServices clothesServices)
        {
            _clothesServices = clothesServices;
        }
        [HttpPost]
        [Route("CreateClothes")]
        public async Task<ActionResult<string>> CreateNewClothes(ClothesDB clothesCreateInfo)
        {
            string result;
            if (clothesCreateInfo == null)
            {
                return BadRequest();
            }
            else
            {
                result = await _clothesServices.CreateClothes(clothesCreateInfo);
                return Ok(result);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<ClothesHalfInfo>>> GetAllClothes()
        {
            var result = await _clothesServices.GetAllClothes();
            return Ok(result);
        }
        [HttpGet("ClothesId")]
        public async Task<ActionResult<ClothesDB>> GetClothesById(int ClothesId)
        {
            var result = await _clothesServices.GetClothesById(ClothesId);
            return Ok(result);
        }
        [HttpGet]
        [Route("Cart")]
        public async Task<ActionResult<ClothesCart>> GetClothesCart (int ClothesId)
        {
            return Ok(await _clothesServices.GetClothesCartById(ClothesId));
        }

    }
}
