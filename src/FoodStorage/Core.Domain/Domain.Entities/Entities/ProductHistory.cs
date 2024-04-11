using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class ProductHistory
    {
       public enum ProductState { Added, WriteOff, Taken };

        public ProductHistory(ProductHistoryId id, ProductId productId, UserId createdBy, DateTime createdAt, ProductState state, int count)
        {
            Id = id;
            ProductId = productId;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            State = state;
            Count = count;   
        }

      public ProductHistoryId Id;
      public  ProductId ProductId;
      public  UserId CreatedBy;
      public  DateTime CreatedAt;
      public  ProductState State;
      public int Count;
    }
}
