using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class CharacteristicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Unit { get; set; }
        public List<string> Value { get; set; }
    }
}
