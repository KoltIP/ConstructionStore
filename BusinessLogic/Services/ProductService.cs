using DataAccess.Entity;
using DataAccess.Methods;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductService
    {
        private ProductMethod _productMethod = new ProductMethod();
        public async Task AddCharacteristic (CharacteristicDto characteristicDto)
        {
            Characteristic characteristic = new Characteristic()
            {
                Name = characteristicDto.Name,
                IdType = characteristicDto.Type,
                Unit = characteristicDto.Unit
            };
            await _productMethod.SaveCharacteristic(characteristic);
        }
        public async Task<bool> AddCategory(CategoryCharacteristicDto categoryCharacteristicDto)
        {
            if (await CheckCategoryName(categoryCharacteristicDto.NameCategory))
            {
                Category category = new Category
                {
                    Name = categoryCharacteristicDto.NameCategory
                };
                await _productMethod.SaveCategory(category);                                
            }
            int? idCategory = await _productMethod.GetCategoryId(categoryCharacteristicDto.NameCategory);            
            foreach (string name in categoryCharacteristicDto.NameCharacteristic)
            {
                int? idCharacteristic = await _productMethod.GetCharacteristicId(name);
                if (idCharacteristic == null)
                {
                    return false;
                }
                if (await CheckCategoryCharacteristic(idCategory, idCharacteristic))
                {
                    CategoryCharacteristic categoryCharacteristic = new CategoryCharacteristic
                    {
                        IdCategory = idCategory,
                        IdCharacteristic = idCharacteristic
                    };
                    await _productMethod.SaveCategoryCharacteristic(categoryCharacteristic);
                }                              
            }
            return true;
        }
        public async Task AddProduct(ProductDto productDto)
        {
            if (await CheckProductName(productDto.Name))
            {
                Product product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Description = productDto.Description,
                    Availability = productDto.Availability,
                    IdCategory = await _productMethod.GetCategoryId(productDto.NameCategory)
                };
                await _productMethod.SaveProduct(product);
            }
            List<Characteristic> characteristics = await _productMethod.GetCharacteristicList(await _productMethod.GetCategoryId(productDto.NameCategory));
            int? idProduct = await _productMethod.GetProductId(productDto.Name);
            for (int i = 0; i<characteristics.Count; i++)
            {                
                string value;
                if (characteristics[i].IdType == 3)
                {
                    if (productDto.Characteristics[i] == "1")
                    {
                        value = "Есть";
                    }
                    else
                        value = "Нет";
                }
                else                
                    value = productDto.Characteristics[i];                
                ProductCharacteristic productCharacteristic = new ProductCharacteristic
                {                    
                    Value = value,
                    IdProduct = idProduct,
                    IdCharacteristic = characteristics[i].Id
                };
                await _productMethod.SaveProductCharacteristic(productCharacteristic);
            }            
        }
        public async Task<List<CharacteristicDto>> GetCharacteristicListDto(string category)
        {
            int? idCategory = await _productMethod.GetCategoryId(category);
            List<Characteristic> characteristics = await _productMethod.GetCharacteristicList(idCategory);
            List<CharacteristicDto> dto = new List<CharacteristicDto>(); 
            foreach (Characteristic characteristic in characteristics)
            {
                CharacteristicDto characteristicDto = new CharacteristicDto
                {
                    Name = characteristic.Name,
                    Type = characteristic.IdType,
                    Unit = characteristic.Unit
                };
                dto.Add(characteristicDto);
            }
            return dto;
        }
        public async Task<List<CharacteristicDto>> GetCharacteristicListDto(int idCategory)
        {
            List<Characteristic> characteristics = await _productMethod.GetCharacteristicList(idCategory);
            List<CharacteristicDto> dto = new List<CharacteristicDto>();
            foreach (Characteristic characteristic in characteristics)
            {
                if (characteristic.IdType == 1)
                {
                    List<ProductCharacteristic> productCharacteristics = await _productMethod.GetProductCharacteristicListByIdCharacteristic(characteristic.Id);
                    List<int> values = new List<int>();
                    foreach (ProductCharacteristic productCharacteristic in productCharacteristics)
                    {                        
                        values.Add(Convert.ToInt32(productCharacteristic.Value));
                    }
                    int max = values.Max();
                    int min = values.Min();
                    values.Clear();
                    values.Add(max);
                    values.Add(min);
                    CharacteristicDto characteristicDto = new CharacteristicDto
                    {
                        Id = characteristic.Id,
                        Name = characteristic.Name,
                        Type = characteristic.IdType,
                        Unit = characteristic.Unit,
                        Value = values.Select(i => i.ToString()).ToList()
                    };
                    dto.Add(characteristicDto);
                }
                else
                {
                    if (characteristic.IdType == 2)
                    {
                        List<ProductCharacteristic> productCharacteristics = await _productMethod.GetProductCharacteristicListByIdCharacteristic(characteristic.Id);
                        List<string> values = new List<string>();
                        foreach (ProductCharacteristic productCharacteristic in productCharacteristics)
                        {
                            if (!values.Contains(productCharacteristic.Value))
                                values.Add(productCharacteristic.Value);
                        }
                        CharacteristicDto characteristicDto = new CharacteristicDto
                        {
                            Id = characteristic.Id,
                            Name = characteristic.Name,
                            Type = characteristic.IdType,
                            Unit = characteristic.Unit,
                            Value = values
                        };
                        dto.Add(characteristicDto);
                    }
                    else
                    {
                        CharacteristicDto characteristicDto = new CharacteristicDto
                        {
                            Id = characteristic.Id,
                            Name = characteristic.Name,
                            Type = characteristic.IdType,
                            Unit = characteristic.Unit
                        };
                        dto.Add(characteristicDto);
                    }
                }                    
            }
            return dto;
        }
        public async Task<List<CatalogDto>> GetCategoryList()
        {
            List<Category> categories = await _productMethod.GetCategoyList();
            List<CatalogDto> dto = new List<CatalogDto>();
            foreach(Category category in categories)
            {
                CatalogDto catalogDto = new CatalogDto
                {
                    categoryid = category.Id,
                    categoryname = category.Name
                };
                dto.Add(catalogDto);
            }
            return dto;
        }
        public async Task<List<CatalogDto>> GetProductList(int idcategory)
        {
            List<Product> products = await _productMethod.GetProductList(idcategory);
            List<CatalogDto> dto = new List<CatalogDto>();            
            foreach(Product product in products)
            {
                CatalogDto catalogDto = new CatalogDto
                {
                    productid = product.Id,
                    productname = product.Name,
                    price = product.Price
                };
                dto.Add(catalogDto);
            }
            return dto;
        }
        public async Task<List<CatalogDto>> GetProductFilter(List<FilterDto> filterDto)
        {
            List<CatalogDto> dto = new List<CatalogDto>();
            List<int?> idProductList = new List<int?>();
            if (filterDto.Count == 1)
            {
                idProductList = await GetIdListByFilter(filterDto[0]);
                List<Product> products = await _productMethod.GetProductsByListId(idProductList);
                foreach (Product product in products)
                {
                    CatalogDto catalogDto = new CatalogDto
                    {
                        productid = product.Id,
                        productname = product.Name,
                        price = product.Price,
                        categoryid = product.IdCategory,
                    };
                    dto.Add(catalogDto);
                }
            }
            else
            {
                idProductList = await GetIdListByFilter(filterDto[0]);
                filterDto.RemoveAt(0);
                foreach (FilterDto filter in filterDto)
                {
                    List<int?> new_idProductList = await GetIdListByFilter(filter);
                    IEnumerable<int?> tmp = idProductList.Intersect(new_idProductList);
                    idProductList = tmp.ToList();
                }
                List<Product> products = await _productMethod.GetProductsByListId(idProductList);
                foreach (Product product in products)
                {
                    CatalogDto catalogDto = new CatalogDto
                    {
                        productid = product.Id,
                        productname = product.Name,
                        price = product.Price,
                        categoryid = product.IdCategory,
                    };
                    dto.Add(catalogDto);
                }
            }
            return dto;
        }
        private async Task<List<int?>> GetIdListByFilter (FilterDto filter)
        {
            List<int?> result = new List<int?>();
            if (filter.Type == 1)
            {
                List<ProductCharacteristic> productCharacteristics = await _productMethod.GetProductCharacteristicList(filter.Id, filter.Min, filter.Max);
                foreach (ProductCharacteristic productCharacteristic in productCharacteristics)
                {
                    result.Add(productCharacteristic.IdProduct);
                }
            }
            else
            {
                List<ProductCharacteristic> productCharacteristics = await _productMethod.GetProductCharacteristicList(filter.Id, filter.Value);
                foreach (ProductCharacteristic productCharacteristic in productCharacteristics)
                {
                    result.Add(productCharacteristic.IdProduct);
                }
            }            
            return result;
        }
        public async Task<CatalogDto> GetProduct(int idProduct)
        {
            Product product = await _productMethod.GetProductById(idProduct);
            Dictionary<string, string> characteristics = new Dictionary<string, string>();
            List<ProductCharacteristic> productCharacteristics = await _productMethod.GetProductCharacteristicListByIdProduct(idProduct);
            List<int?> idCharacteristic = new List<int?>();
            foreach(ProductCharacteristic productCharacteristic in productCharacteristics)
            {
                idCharacteristic.Add(productCharacteristic.IdCharacteristic);
            }
            List<Characteristic> characteristics1 = await _productMethod.GetCharacteristicByListIdProduct(idCharacteristic);
            for(int i = 0; i < idCharacteristic.Count; i++)
            {
                characteristics.Add(characteristics1[i].Name, productCharacteristics[i].Value + ' ' +characteristics1[i].Unit);
            }
            CatalogDto catalogDto = new CatalogDto
            {
                productid = product.Id,
                productname = product.Name,
                price = product.Price,
                availability = product.Availability,
                description = product.Description,
                categoryid = product.IdCategory,
                characteristics = characteristics                
            };
            return catalogDto;
        }

        public async Task<Product> GetProductById(int productId)
        {
            return (await _productMethod.GetProductById(productId));
        }

        public async Task<Dictionary<ProductDto, int>> GetProductsByListId(Dictionary<int,int> basket)
        {
            List<int> idList = new List<int>();
            foreach (KeyValuePair<int, int> keyValue in basket)
            {
                idList.Add(keyValue.Key);
            }
            List<Product> productList =await _productMethod.GetProductsByListId(idList);

            Dictionary<ProductDto, int> result = new Dictionary<ProductDto, int>();
            foreach (Product product in productList)
            {
                ProductDto productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price=product.Price
                };
                


                result.Add(productDto,basket[product.Id]);

            }
            return result;
        }

        private async Task<bool> CheckCategoryName(string name)
        {
            return (await _productMethod.GetCategory(name)) == null;
        }
        private async Task<bool> CheckCategoryCharacteristic(int? idCategory, int? idCharacteristic)
        {
            return (await _productMethod.GetCategoryCharacteristic(idCategory, idCharacteristic)) == null;
        }
        private async Task<bool> CheckProductName(string name)
        {
            return (await _productMethod.GetProduct(name)) == null;
        }

    }
}
