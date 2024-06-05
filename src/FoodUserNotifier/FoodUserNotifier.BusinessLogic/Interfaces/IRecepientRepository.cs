using FoodUserNotifier.BusinessLogic.Contracts;
using FoodUserNotifier.BusinessLogic.Types;
using FoodUserNotifier.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodUserNotifier.BusinessLogic.Interfaces
{
    public interface IRecepientRepository
    {
       public void Create(Recepient recepient);
       public void Update(Recepient recepient);
       public void Delete(Guid id);
       public RecepientDTO GetById(Guid id);
       public IEnumerable<RecepientDTO> GetAllForRole(Role role);
    }
}
