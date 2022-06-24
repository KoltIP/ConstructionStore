using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<CategoryCharacteristic> CategoryCharacteristics { get; set; }
        public Category()
        {
            Products = new HashSet<Product>();
            CategoryCharacteristics = new HashSet<CategoryCharacteristic>();
        }
    }
}
