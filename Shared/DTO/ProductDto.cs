using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public bool ViewAddProduct { get; set; } = false;
        public bool ViewAddCategory { get; set; } = false;
        public bool ViewAddCharacteristic { get; set; } = false;
        public bool ErrorCharacteristic { get; set; } = false;
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Availability { get; set; }
        public string NameCategory { get; set; }
        public string[] Characteristics { get; set; }
    }
}
