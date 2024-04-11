using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entities;
namespace Application.Repositories.Interface
{
    internal interface IProductRepository
    {
        string Create(Product product);
        Product FindById(ProductId productId); 
        Product FindByName(ProductName productName);
        IEnumerable<Product> GetAll();
        void Delete(Product product);
    }
}
