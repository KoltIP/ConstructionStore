using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Characteristic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdType { get; set; }
        public string Unit { get; set; }
        public ICollection<CategoryCharacteristic> CategoryCharacteristics { get; set; }
        public virtual TypeCharacteristic TypeCharacteristic { get; set; }
        public ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
        public Characteristic()
        {
            ProductCharacteristics = new HashSet<ProductCharacteristic>();
            CategoryCharacteristics = new HashSet<CategoryCharacteristic>();
        }

    }
}
