using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class TypeCharacteristic
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Characteristic> Characteristics { get; set; }
        public TypeCharacteristic()
        {
            Characteristics = new HashSet<Characteristic>();
        }
    }
}
