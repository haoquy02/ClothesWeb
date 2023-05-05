using ClothesWeb.Dto.Account;
using ClothesWeb.Models;
using ClothesWeb.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ClothesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("CreateAccount")]
        public async Task<ActionResult<string>> PostCreateAccount(CreateAccountDto userCreateInfo)
        {
            string result;
            if (userCreateInfo == null)
            {
                return BadRequest();
            }
            else
            {
                result = await _accountService.CreateAccount(userCreateInfo);
                return Ok(result);
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAccount(LoginAccountDto userLoginInfo)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = true,
                IsEssential = true,
            };
            if (userLoginInfo == null)
            {
                return BadRequest();
            }
            else
            {
                var result = await _accountService.Login(userLoginInfo);
                if (result.Status == "Login successful")
                {
                    HttpContext.Response.Cookies.Append("token", result.Token, options);
                    HttpContext.Response.Cookies.Append("id", result.Id.ToString(), options);
                }
                return Ok(result);

            }
        }
    }
}
