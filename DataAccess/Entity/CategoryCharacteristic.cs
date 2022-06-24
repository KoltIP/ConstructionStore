using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class CategoryCharacteristic
    {
        public int Id { get; set; }
        public int? IdCharacteristic { get; set; }
        public int? IdCategory { get; set; }
        public virtual Category Category { get; set; }
        public virtual Characteristic Characteristic { get; set; }        
    }
}
