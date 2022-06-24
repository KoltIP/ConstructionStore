using BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("/Login")]
    public class LoginController : Controller
    {
        private UserService _user = new UserService();
        public IActionResult Register()
        {
            return Redirect("/Registration");
        }
        [HttpGet]
        [Route("")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            LoginDto loginDto = new LoginDto
            {
                Login = login,
                Password = password
            };
            int? id = await _user.LoginUser(loginDto);
            if (!loginDto.Error)
            {
                HttpContext.Session.SetInt32("UserId", id.Value);
                if (!HttpContext.Session.Keys.Contains("Basket"))
                {
                    BasketDto basketDto = new BasketDto();
                    string json = JsonSerializer.Serialize<BasketDto>(basketDto);
                    HttpContext.Session.SetString("Basket", json);                   
                }
                return Redirect("/");
            }
            UserModel userModel = new UserModel();
            userModel.LoginDto = loginDto;
            return View(userModel);
        }
    }
}
