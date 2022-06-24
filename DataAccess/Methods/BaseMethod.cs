using DataAccess.Entity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Methods
{
    public class BaseMethod
    {
        public ConstructionStoreDb GetDb()
        {
            return new ConstructionStoreDb(SystemConfiguration.Configuration["ConnectionString"]);
        }
    }
}
