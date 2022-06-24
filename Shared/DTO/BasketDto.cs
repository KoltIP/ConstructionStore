using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class BasketDto
    {
        public Dictionary<int, int> Products { get; set; } = new Dictionary<int, int>();

    }
}
