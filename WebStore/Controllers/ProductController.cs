using BusinessLogic.Services;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("/AddProduct")]
    public class ProductController : Controller
    {
        private ProductService _productService = new ProductService();
        private UserModel _userModel = new UserModel();
        private ProductDto _productDto = new ProductDto();
        [HttpGet]
        [Route("")]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpGet]
        [Route("/ViewAddProduct")]
        public IActionResult ViewAddProduct()
        {
            _productDto.ViewAddProduct = true;
            _userModel.ProductDto= _productDto;
            return View("AddProduct", _userModel);
        }
        [HttpGet]
        [Route("/ViewAddCategory")]
        public IActionResult ViewAddCategory()
        {
            _productDto.ViewAddCategory = true;
            _userModel.ProductDto = _productDto;
            return View("AddProduct", _userModel);
        }
        [HttpGet]
        [Route("/ViewAddCharacteristic")]
        public IActionResult ViewAddCharacteristic()
        {
            _productDto.ViewAddCharacteristic = true;
            _userModel.ProductDto = _productDto;
            return View("AddProduct", _userModel);
        }
        [HttpPost]
        [Route("/ViewAddCharacteristic")]
        public async Task<IActionResult> AddCharacteristic(int type, string name, string unit)
        {
            CharacteristicDto characteristicDto = new CharacteristicDto
            {
                Type = type,
                Name = name,
                Unit = unit
            };
            await _productService.AddCharacteristic(characteristicDto);
            return Redirect("/AddProduct");
        }
        [HttpPost]
        [Route("/ViewAddCategory")]
        public async Task<IActionResult> AddCategory(string name, string[] nameCharacteristic)
        {
            CategoryCharacteristicDto categoryCharacteristicDto = new CategoryCharacteristicDto
            {
                NameCategory = name,
                NameCharacteristic = nameCharacteristic
            };
            if (!await _productService.AddCategory(categoryCharacteristicDto))
            {
                _productDto.ViewAddCategory = true;
                _productDto.ErrorCharacteristic = true;
                _userModel.ProductDto = _productDto;
                return View("AddProduct", _userModel);
            }
            return Redirect("/AddProduct");
        }
        [HttpGet]
        [Route("/ViewCharacteristic")]
        public async Task<List<CharacteristicDto>> AddCharacteristicProduct ([FromQuery] string category) 
        {
            return await _productService.GetCharacteristicListDto(category);            
        }
        [HttpPost]
        [Route("/ViewAddProduct")]
        public async Task<IActionResult> AddProduct(string name, float price, string description, int availability, string category, string[] characterictics)
        {            
            _productDto.Name = name;
            _productDto.Price = price;
            _productDto.Description = description;
            _productDto.Availability = availability;
            _productDto.NameCategory = category;
            _productDto.Characteristics = characterictics;
            await _productService.AddProduct(_productDto);
            return Redirect("/AddProduct");
        }
    }
}
