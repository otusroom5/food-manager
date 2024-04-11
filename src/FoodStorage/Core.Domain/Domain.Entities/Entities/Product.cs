using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class Product
    {

        public ProductId Id;
        public ProductName Name;
        public string Description;
        public int MinAmountPerDay;
        public int ExpiryDateNotificationHours;
        public Product(ProductId id, ProductName name, string description, int minAmountPerDay, int expiryDateNotificationHours) 
        {
            Id = id;
            Name = name;
            Description = description;
            MinAmountPerDay = minAmountPerDay;
            ExpiryDateNotificationHours = expiryDateNotificationHours;

        }
    }
}
