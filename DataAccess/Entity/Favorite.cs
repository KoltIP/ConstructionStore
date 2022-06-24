using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Favorite
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdProduct { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
