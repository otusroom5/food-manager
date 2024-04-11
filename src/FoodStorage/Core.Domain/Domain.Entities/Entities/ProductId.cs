using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class ProductId
    {
        Guid Value { get; set; }

       public ProductId() 
        {
            Value = Guid.NewGuid();
        }

    }
}
