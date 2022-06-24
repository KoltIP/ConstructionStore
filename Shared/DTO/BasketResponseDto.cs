using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class BasketResponseDto
    {
        public Dictionary<ProductDto, int> basketResponseDto { get; set; } = new Dictionary<ProductDto, int>();
        //private int cost = costCalculation();
        public float costCalculation()
        {
            float sum = 0;
            foreach (var item in this.basketResponseDto)
            {
                sum += item.Key.Price*item.Value;
            }
            return sum;
        }
    }
}
