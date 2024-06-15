using FoodManager.Shared.Types;
using FoodUserNotifier.Application.WebAPI.Contracts;
using FoodUserNotifier.Application.WebAPI.Interfaces;
using FoodUserNotifier.Application.WebAPI.Models;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Entities.Types;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;




namespace FoodUserNotifier.Application.WebAPI.Controllers
{
    [Route("[controller]/[RecepientRepositoryController]/")]
    [ApiController]
    public class RecepientController : ControllerBase, IRecepientController
    {

        private readonly IRecepientRepository recepientRepository;

        RecepientController(IServiceProvider _recepientRepository) 
        {
            recepientRepository = _recepientRepository.GetService<IRecepientRepository>(); 
        }

        [HttpGet("{Id}/{RecepientId}/{ContactType}/{Address}")]
        public void CreateRecepient([FromRoute] Guid Id, [FromRoute] Guid RecepientId, [FromRoute] ContactType ContactType, [FromRoute] string Address)
        {
          Recepient recepient = new Recepient();
          recepient.Id = Id;
          recepient.RecepientId = RecepientId;
          recepient.ContactType = ContactType;
          recepient.Address = Address;
          recepientRepository.CreateRecepient(recepient);
        }

        [HttpGet("{Id}")]
        public void DeleteRecepient([FromRoute] Guid id)
        {
            recepientRepository.DeleteRecepient(id);
        }

        [HttpGet("{Id}")]
        public  Recepient GetRecepientById(Guid id) 
        {
            Task<Recepient> recepient =  recepientRepository.GetRecepientById(id);
            return recepient.Result;

        }

        [HttpGet("{Id}/{RecepientId}/{ContactType}/{Address}")]
        public void UpdateRecepient([FromRoute] Guid Id, [FromRoute] Guid RecepientId, [FromRoute] ContactType ContactType, [FromRoute] string Address)
        {
            Recepient recepient = new Recepient();
            recepient.Id = Id;
            recepient.RecepientId = RecepientId;
            recepient.ContactType = ContactType;
            recepient.Address = Address;
            recepientRepository.UpdateRecepient(recepient);
        }
    }
}
