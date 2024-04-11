using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class UserId
    {
        public Guid Value { get; set; }
        public UserId() 
        {
            Value = Guid.NewGuid();
        }

    }
}
