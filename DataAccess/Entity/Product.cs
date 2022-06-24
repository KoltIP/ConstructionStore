using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Availability { get; set; }
        public int? IdCategory { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
        public ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
        public ICollection<Favorite> Favorites { get; set; }

        public Product()
        {
            ProductOrders = new HashSet<ProductOrder>();
            ProductReviews = new HashSet<ProductReview>();
            ProductCharacteristics = new HashSet<ProductCharacteristic>();
            Favorites = new HashSet<Favorite>();
        }
    }
}
