using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodUserNotifier.Core.Interfaces.Repositories
{
    public interface IRecepientRepository
    {
        public void CreateRecepient(Recepient recepient);
        public void UpdateRecepient(Recepient recepient);
        public void DeleteRecepient(Guid id);
        public Task<Recepient> GetRecepientById(Guid id);
        public IEnumerable<Recepient> GetAllForRoleRecepient(Role role);

    }
}
