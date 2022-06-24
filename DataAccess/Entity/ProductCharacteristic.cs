using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class ProductCharacteristic
    {
        public int Id { get; set; }
        public string Value { get; set;}
        public int? IdProduct { get; set; }
        public int? IdCharacteristic { get; set; }
        public virtual Product Product { get; set; }
        public virtual Characteristic Characteristic { get; set; }
    }
}
