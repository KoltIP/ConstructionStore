using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class ProductReview
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public int Rating { get; set; }
        public int? IdProduct { get; set; }
        public int? IdUser { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
