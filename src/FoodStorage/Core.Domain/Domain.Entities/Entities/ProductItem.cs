using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
   public class ProductItem
    {
       public ProductItem(ProductItemId id, ProductId productId, int amount, DateTime creatingDate, DateTime expiryDate) 
        {
            Id = id;
            ProductId = productId;
            Amount = amount;
            CreatingDate = creatingDate;
            ExpiryDate = expiryDate;
        }

       public ProductItemId Id { get; set; }
       public ProductId ProductId { get; set; }
       public int Amount { get; set; }
       public  DateTime CreatingDate { get; set; }
       public DateTime ExpiryDate { get; set; }
    }
}
