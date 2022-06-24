using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;

namespace DataAccess.Data
{
    public class CreateTypeCharacteristic
    {
        public static void Initialize(ConstructionStoreDb context)
        {
            context.Database.EnsureCreated();
            
            if (context.TypeCharacteristics.Any())
            {
                return;
            }
            var types = new TypeCharacteristic[] {
            new TypeCharacteristic {Type="Числовой"},
            new TypeCharacteristic {Type="Строковый"},
            new TypeCharacteristic {Type="Есть/нет"}
            };
            foreach (TypeCharacteristic type in types)
            {
                context.TypeCharacteristics.Add(type);
            }
            context.SaveChanges();
        }
        public static void Add(ConstructionStoreDb context, TypeCharacteristic typeCharacteristic)
        {
            context.TypeCharacteristics.Add(typeCharacteristic);
            context.SaveChanges();
        }
    }
}
