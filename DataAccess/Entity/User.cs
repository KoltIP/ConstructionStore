using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserTypeEnum Type { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public User()
        {
            Orders = new HashSet<Order>();
            ProductReviews = new HashSet<ProductReview>();
            Favorites = new HashSet<Favorite>();
        }
    }
    
}
