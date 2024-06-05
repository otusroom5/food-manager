using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.DataAccess.Infrastructure.EntityFramework;
using FoodUserNotifier.BusinessLogic.Entities;
using FoodUserNotifier.BusinessLogic.Types;
using Microsoft.EntityFrameworkCore;
using FoodUserNotifier.BusinessLogic.Contracts;
using System.Data;
namespace FoodUserNotifier.DataAccess.Implementations
{
    public class RecepientRepository : IRecepientRepository
    {
        DatabaseContext _databaseContext;

        public RecepientRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public void Create(Recepient recepient)
        {
            RecepientDTO recepientDTO = recepient.ToDTO(recepient);
            _databaseContext.Recipient.Add(recepientDTO);
            _databaseContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _databaseContext.Recipient.RemoveRange(_databaseContext.Recipient.Where(recepient => recepient.Id == id));
            _databaseContext.SaveChanges();
        }

        public IEnumerable<RecepientDTO> GetAllForRole(Role role)
        {
          return  _databaseContext.Recipient.Where(recepient => recepient.RoleEnum == role.ToString()).ToList();
        }

        public RecepientDTO GetById(Guid id)
        {
            return _databaseContext.Recipient.Find(id);
        }

        public void Update(Recepient _recepient)
        {
            RecepientDTO recepientDTO = _databaseContext.Recipient.Single(recepient => recepient.Email == _recepient.Email);
            recepientDTO.RoleEnum = _recepient.RoleEnum.ToString();
            _databaseContext.SaveChanges();
        }
    }
}
