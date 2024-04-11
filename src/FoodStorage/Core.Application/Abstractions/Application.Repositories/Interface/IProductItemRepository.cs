using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entities;

namespace Application.Repositories.Interface
{
    internal interface IProductItemRepository
    {
        public ProductItemId Create(ProductItem productItem);
        public ProductItem FinfById(ProductItemId productItemId);
        public IEnumerable<ProductItem> GetByProductName(ProductName productName);
        public IEnumerable<ProductItem> GetAll();
        public void Change(ProductItem productItem);
        public void Delete(ProductItem productItem);


    }
}
