using FoodUserNotifier.BusinessLogic.Interfaces;
using FoodUserNotifier.WebApi.Extensions;
using FoodUserNotifier.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodUserNotifier.WebApi.DataControllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class RecepientsController : Controller
{
    private readonly IRecepientService _recepientService;
    public RecepientsController(IRecepientService recepientService)
    {
        _recepientService  = recepientService;
    }

    /// <summary>
    /// Возвращает всех получателей
    /// </summary>
    /// <param name="item"></param>
    /// <returns>List of users</returns>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpGet]
    public ActionResult<IEnumerable<RecepientModel>> GetAll()
    {
        return Ok(_recepientService.GetAll().Select(item=>item.ToModel()));
    }

    /// <summary>
    /// Добавляет получателя
    /// </summary>
    /// <param name="item">User</param>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpPut]
    public ActionResult Create(RecepientModel item)
    {
        try
        {
            _recepientService.Create(item.ToDto());
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Обновляет получателя
    /// </summary>
    /// <param name="item"></param>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpPost]
    public ActionResult Update(RecepientModel item)
    {
        try
        {
            _recepientService.Update(item.ToDto());
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Удаляет получателя
    /// </summary>
    /// <param name="item"></param>
    /// <response code="200">Success</response>
    /// <response code="400">If error</response>
    [HttpDelete]
    public ActionResult Delete(RecepientModel item)
    {
        try
        {
            _recepientService.Delete(item.Id);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
