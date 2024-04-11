using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entities;

namespace Application.Repositories.Interface
{
    internal interface IProductHistoryRepository
    {
        ProductHistoryId Create(ProductHistory productHistory);
        ProductHistory FindById(ProductHistory productHistory);
        IEnumerable<ProductHistory> GetByProductName(ProductName productName);
        IEnumerable<ProductHistory> GetAll();
        void Delete(ProductHistory productHistory);
    }
}
