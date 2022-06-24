using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Methods
{
    public class ProductMethod:BaseMethod
    {
        public async Task<Product> SaveProduct(Product product)
        {
            using (var database = GetDb())
            {
                await database.Products.AddAsync(product);
                await database.SaveChangesAsync();
                return product;
            }
        }
        public async Task<ProductCharacteristic> SaveProductCharacteristic(ProductCharacteristic productCharacteristic)
        {
            using (var database = GetDb())
            {
                await database.ProductCharacteristics.AddAsync(productCharacteristic);
                await database.SaveChangesAsync();
                return productCharacteristic;
            }
        }
        public async Task<Characteristic> SaveCharacteristic(Characteristic characteristic)
        {
            using (var database = GetDb())
            {
                await database.Characteristics.AddAsync(characteristic);
                await database.SaveChangesAsync();
                return characteristic;
            }
        }
        public async Task<Category> SaveCategory(Category category)
        {
            using (var database = GetDb())
            {
                await database.Categorys.AddAsync(category);
                await database.SaveChangesAsync();
                return category;
            }
        }
        public async Task<CategoryCharacteristic> SaveCategoryCharacteristic(CategoryCharacteristic categoryCharacteristic)
        {
            using (var database = GetDb())
            {
                await database.CategoryCharacteristics.AddAsync(categoryCharacteristic);
                await database.SaveChangesAsync();
                return categoryCharacteristic;
            }
        }
        public async Task<Category> GetCategory(string name)
        {
            using (var database = GetDb())
            {
                return await database.Categorys.Where(category => category.Name == name).FirstOrDefaultAsync();
            }
        }
        public async Task<int?> GetCategoryId(string name)
        {
            if (await GetCategory(name) != null)
            {
                return (await GetCategory(name)).Id;
            }
            return null;
        }
        public async Task<Characteristic> GetCharacteristic(string name)
        {
            using (var database = GetDb())
            {
                return await database.Characteristics.Where(category => category.Name == name).FirstOrDefaultAsync();
            }
        }
        public async Task<int?> GetCharacteristicId(string name)
        {
            if (await GetCharacteristic(name) != null)
            {
                return (await GetCharacteristic(name)).Id;
            }
            return null;
        }
        public async Task<CategoryCharacteristic> GetCategoryCharacteristic(int? idCategory, int? idCharacteristic)
        {
            using (var database = GetDb())
            {
                return await database.CategoryCharacteristics.Where(id => id.IdCategory == idCategory && id.IdCharacteristic == idCharacteristic).FirstOrDefaultAsync();
            }
        }        
        public async Task<List<Characteristic>> GetCharacteristicList(int? idCategory)
        {
            using (var database = GetDb())
            {
                List<Characteristic> characteristicList = new List<Characteristic>();
                List<CategoryCharacteristic> categoryCharacteristics = await database.CategoryCharacteristics.Where(category => category.IdCategory == idCategory).ToListAsync();
                foreach(CategoryCharacteristic categoryCharacteristic in categoryCharacteristics)
                {
                    Characteristic characteristic = await database.Characteristics.Where(characteristic => characteristic.Id == categoryCharacteristic.IdCharacteristic).FirstOrDefaultAsync();
                    characteristicList.Add(characteristic);
                }
                return characteristicList;
            }
        }
        public async Task<Product> GetProduct(string name)
        {
            using (var database = GetDb())
            {
                return await database.Products.Where(product => product.Name == name).FirstOrDefaultAsync();
            }
        }
        public async Task<int?> GetProductId(string name)
        {
            return (await GetProduct(name)).Id;
        }

        public async Task<List<Product>> GetProductsByListId(List<int> idList)
        {
            using (var database = GetDb())
            {
                return await database.Products.Where(id => idList.Contains(id.Id)).ToListAsync();
            }
        }
        
        public async Task<List<Category>> GetCategoyList()
        {
            using (var database = GetDb())
            {
                return await database.Categorys.ToListAsync();
            }
        }
        public async Task<List<Product>> GetProductList(int idcategory)
        {
            using (var database = GetDb())
            {
                return await database.Products.Where(product => product.IdCategory == idcategory).ToListAsync();
            }
        }
        public async Task<List<ProductCharacteristic>> GetProductCharacteristicListByIdCharacteristic (int? idCharacteristic)
        {
            using (var database = GetDb())
            {
                return await database.ProductCharacteristics.Where(id => id.IdCharacteristic == idCharacteristic).ToListAsync();
            }
        }
        public async Task<List<ProductCharacteristic>> GetProductCharacteristicListByIdProduct(int? idProduct)
        {
            using (var database = GetDb())
            {
                return await database.ProductCharacteristics.Where(id => id.IdProduct == idProduct).ToListAsync();
            }
        }
        public async Task<List<ProductCharacteristic>> GetProductCharacteristicList(int? idCharacteristic, string value)
        {
            using (var database = GetDb())
            {
                return await database.ProductCharacteristics.Where(productCharacteristic => productCharacteristic.IdCharacteristic == idCharacteristic && productCharacteristic.Value == value).ToListAsync();
            }
        }
        public async Task<List<ProductCharacteristic>> GetProductCharacteristicList(int? idCharacteristic, int? min, int? max)
        {
            using (var database = GetDb())
            {
                return await database.ProductCharacteristics.Where(productCharacteristic => productCharacteristic.IdCharacteristic == idCharacteristic && ((min <= Convert.ToInt32(productCharacteristic.Value)) && (Convert.ToInt32(productCharacteristic.Value) <= max))).ToListAsync();
            }
        }        
        public async Task<List<Characteristic>> GetCharacteristicByListIdProduct(List<int?> idList)
        {
            using (var database = GetDb())
            {
                return await database.Characteristics.Where(id => idList.Contains(id.Id)).ToListAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            using (var database = GetDb())
            {
                return await database.Products.Where(product => product.Id == id).FirstOrDefaultAsync();
            }
        }
        
        public async Task<List<Product>> GetProductsByListId(List<int?> idList)
        {
            using (var database = GetDb())
            {
                return await database.Products.Where(id => idList.Contains(id.Id)).ToListAsync();
            }
        }
    }
}
