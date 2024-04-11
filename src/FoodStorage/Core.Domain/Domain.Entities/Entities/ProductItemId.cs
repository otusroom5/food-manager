using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class ProductItemId
    {
        Guid Value { get; set; }
       public ProductItemId() 
        {
            Value = Guid.NewGuid();
        }

    }
}
