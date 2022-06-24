using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderStatus { get; set; }
        public DateTime EndOfReserve { get; set; }
        public virtual User User { get; set; }
        public ICollection<ProductOrder> PruductOrders { get; set; }
        public Order()
        {
            PruductOrders = new HashSet<ProductOrder>();
        }
    }
}
