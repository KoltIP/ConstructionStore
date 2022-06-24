using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Shared.DTO;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("/Basket")]
    public class BasketController : Controller
    {
        private ProductService _productService = new ProductService();
        private BasketDto _basketDto = new BasketDto();

        private void InitializeBasket()
        {
            if (!HttpContext.Session.Keys.Contains("Basket"))
            {
                BasketDto basketDto = new BasketDto();
                string json = JsonSerializer.Serialize<BasketDto>(basketDto);
                HttpContext.Session.SetString("Basket", json);
            }
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Basket()
        {
            InitializeBasket();

            int? id = HttpContext.Session.GetInt32("UserId");
            _basketDto = JsonSerializer.Deserialize<BasketDto>(HttpContext.Session.GetString("Basket"));
            BasketResponseDto basketResponseDto = new BasketResponseDto();

            basketResponseDto.basketResponseDto = await _productService.GetProductsByListId(_basketDto.Products);
                
            UserModel userModel = new UserModel();
            userModel.ID = id;
            userModel.BasketResponsetDto = basketResponseDto;
            return View(userModel);
        }


        [HttpGet]
        [Route("/AddProductInBasket")]
        public void Add(int idProduct)
        {
            InitializeBasket();
            _basketDto = JsonSerializer.Deserialize<BasketDto>(HttpContext.Session.GetString("Basket"));
            if (_basketDto.Products.ContainsKey(idProduct))
            {                
                _basketDto.Products[idProduct] = _basketDto.Products[idProduct]+1;
            }
            else
            {
                _basketDto.Products.Add(idProduct, 1);                
            }
            string json = JsonSerializer.Serialize(_basketDto);
            HttpContext.Session.SetString("Basket", json);
        }

        [HttpPost]
        [Route("/CountProduct/{id}")]
        public void Counter([FromBody] BasketProductDto basketProductDto,[FromRoute] int id)
        {
            _basketDto = JsonSerializer.Deserialize<BasketDto>(HttpContext.Session.GetString("Basket"));
            _basketDto.Products[id] = basketProductDto.Count;
            string json = JsonSerializer.Serialize(_basketDto);
            HttpContext.Session.SetString("Basket", json);
        }

        [HttpPost]
        [Route("/RemoveProduct/{id}")]
        public void Remover(int id)
        {
            _basketDto = JsonSerializer.Deserialize<BasketDto>(HttpContext.Session.GetString("Basket"));
            _basketDto.Products.Remove(id);
            string json = JsonSerializer.Serialize(_basketDto);
            HttpContext.Session.SetString("Basket", json);
        }
    }
}
