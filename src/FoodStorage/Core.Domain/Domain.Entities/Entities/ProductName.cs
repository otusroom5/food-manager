using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class ProductName
    { 
        public string Value { get; set; }
        public ProductName( string value )
        {
            Value = value;
        }
    }
}
