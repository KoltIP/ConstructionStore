using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class OrderController : Controller
    {
        [Route("/Order")]
        [HttpGet]
        public IActionResult Order()
        {
            
            int? id = HttpContext.Session.GetInt32("UserId");
            UserModel userModel = new UserModel();
            userModel.ID = id;
            if (id.HasValue)
            {                
                return View(userModel);
            }
            else
            {
                return Redirect("/Registration");
            }
        }
    }
}
