using BusinessLogic.Services;
using DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    
    [Route("/Catalog")]
    public class CatalogController : Controller
    {
        UserService _userService = new UserService();
        private ProductService _productService = new ProductService();
        private UserModel _userModel = new UserModel();
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Catalog()
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            _userModel.ID = id;
            UserTypeEnum type = await _userService.GetUserType(id);
            if (type == UserTypeEnum.Admin)
            {
                _userModel.Type = 1;
            }
            else
                _userModel.Type = 0;
            return View(_userModel);
        }

        [HttpGet]
        [Route("/CatalogCategories")]
        public async Task<List<CatalogDto>> ViewCatalog()
        {
            return await _productService.GetCategoryList();
        }

        [HttpGet]
        [Route("/CatalogProducts")]
        public async Task<List<CatalogDto>> ViewProduct(int id)
        {
            return await _productService.GetProductList(id);
        }

        [HttpGet]
        [Route("/CatalogProductsFilter")]
        public async Task<List<CharacteristicDto>> ViewProductFilter(int id)
        {
            return await _productService.GetCharacteristicListDto(id);
        }

        [HttpPost]
        [Route("/FiltrationProduct")]
        public async Task<List<CatalogDto>> FiltrationProduct([FromBody] List<FilterDto> filterDto)
        {
            return await _productService.GetProductFilter(filterDto); ;
        }

        [HttpGet]
        [Route("/Product/{id}")]
        public async Task<CatalogDto> GetProduct([FromRoute] int id)
        {
            return await _productService.GetProduct(id);
        }

        [HttpGet]
        [Route("/ViewProduct/{id}")]
        public IActionResult ViewProductInfo([FromRoute] int id)
        {
            int? idUser = HttpContext.Session.GetInt32("UserId");
            _userModel.ID = idUser;
            _userModel.IdProduct = id;
            return View("../Shared/Product", _userModel);
        }
    }
}
