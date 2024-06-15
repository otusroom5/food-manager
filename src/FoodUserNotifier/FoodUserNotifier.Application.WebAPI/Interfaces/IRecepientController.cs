using FoodUserNotifier.Core.Entities.Types;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Infrastructure.Repositories.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodUserNotifier.Application.WebAPI.Interfaces
{
    public interface IRecepientController
    {
        public void CreateRecepient( Guid Id,  Guid RecepientId,  ContactType ContactType, string Address);
        public void DeleteRecepient( Guid id);
        public Recepient GetRecepientById(Guid id);
        public void UpdateRecepient(Guid Id, Guid RecepientId, ContactType ContactType, string Address);
    }
}
