using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class CatalogDto
    {
        public int? categoryid { get; set; }
        public string categoryname { get; set; }
        public int? productid { get; set; }
        public string productname { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public int availability { get; set; } 
        public Dictionary<string,string> characteristics { get; set; }
    }    
}
