using BusinessLogic.Services;
using DataAccess.Entity;
using DataAccess.Methods;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("/Registration")]
    public class RegistrationController : Controller
    {
        private UserService _user = new UserService();

        public IActionResult Login()
        {
            return Redirect("/Login");
        }
        [HttpGet]
        [Route("")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string surName, string name, string middleName, string telephone, string email, string password, string login)
        {
            RegistrationDto registration = new RegistrationDto
            {
                Name = name,
                SurName = surName,
                MiddleName = middleName,
                Mail = email,
                Login = login,
                Telephone = telephone,
                Password = password
            };
            await _user.RegisterUser(registration);
            if (!registration.ErrorEmail && !registration.ErrorLogin && !registration.ErrorTel)
            {
                return Redirect("/");
            }
            UserModel userModel = new UserModel();
            userModel.RegistrationDto = registration;
            return View(userModel);
        }
    }
}
