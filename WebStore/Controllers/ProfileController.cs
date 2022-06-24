using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("/Profile")]
    public class ProfileController : Controller
    {
        private UserService _user = new UserService();
        

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Profiler()
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            if (id.HasValue)
            {
                User user = await _user.ProfileUser(id);
                ProfileDto profileDto = new ProfileDto
                {
                    Name = user.Name,
                    Surname = user.SurName,
                    Patronimic = user.MiddleName,
                    Email = user.Mail,
                    Phone = user.Telephone,
                    Flag = false
                };
                UserModel userModel = new UserModel();
                userModel.ID = id;
                userModel.ProfileDto = profileDto;
                return View(userModel);
            }
            return Redirect("/Registration");
        }

        [HttpPost]
        [Route("/LogOut")]
        public void ProfileExit()
        {
            HttpContext.Session.Clear();
        }

        [HttpDelete]
        [Route("")]
        public IActionResult ProfileDelete()
        {
            //Implementation
            int? id = HttpContext.Session.GetInt32("UserId");
            Task<bool> Ok = _user.DeleteProfileUser(id);
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        [HttpGet]
        [Route("/ChangeProfile")]
        public async Task<IActionResult> ProfilerChange()
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            User user = await _user.ProfileUser(id);
            ProfileDto profileDto = new ProfileDto
            {
                Name = user.Name,
                Surname = user.SurName,
                Patronimic = user.MiddleName,
                Email = user.Mail,
                Phone = user.Telephone,
                OldPassword = user.Password,
                Flag = true
            };
            UserModel userModel = new UserModel();
            userModel.ID = id;
            userModel.ProfileDto = profileDto;
            return View("/Views/Shared/Profiler.cshtml", userModel);
        }

        [HttpPost]
        [Route("/ChangeProfile")]
        public async Task<IActionResult> ProfilerSave(string name, string surName, string patronimic, string telephone, string email, string oldPassword, string newPassword)
        {
            UserModel userModel = new UserModel();
            ProfileDto profile = new ProfileDto
            {
                Name = name,
                Surname = surName,
                Patronimic = patronimic,
                Email = email,
                Phone = telephone,
                OldPassword = oldPassword,
                NewPassword = newPassword,
                Flag = true
            };
            int? id = HttpContext.Session.GetInt32("UserId");
            userModel.ID = id;
            User user = await _user.ProfileUser(id);
            await _user.ChangeProfileUser(user, profile);
            if (!profile.ErrorEmail && !profile.ErrorTel && !profile.ErrorPassword)
            {
                profile.Flag = false;
            }

            userModel.ProfileDto = profile;
            return View("/Views/Shared/Profiler.cshtml", userModel);         
        }
    }
}
