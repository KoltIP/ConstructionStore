using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class FilterDto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int? Max { get; set; }
        public int? Min { get; set; }
        public string Value { get; set; }
    }
}
