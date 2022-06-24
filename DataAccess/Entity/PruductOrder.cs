using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class PruductOrder
    {
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Id_User { get; set; }
    }
}
