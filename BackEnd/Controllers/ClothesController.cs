using ClothesWeb.Dto.Account;
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
        [Route("CreatePost")]
        public async Task<ActionResult<string>> PostCreatNewPost(ClothesDB clothesCreateInfo)
        {
            string result;
            if (clothesCreateInfo == null)
            {
                return BadRequest();
            }
            else
            {
                result = await _clothesServices.CreatePost(clothesCreateInfo);
                return Ok(result);
            }
        }

    }
}
