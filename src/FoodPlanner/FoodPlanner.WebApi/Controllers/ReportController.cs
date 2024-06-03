using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Services;
using FoodPlanner.DataAccess.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ReportController : ControllerBase
    {        
        private readonly ILogger<ReportController> _logger;
        public ReportController(ILogger<ReportController> logger)
        {            
            _logger = logger;
        }

        [HttpPost("Create")]
        public ActionResult<Guid> Create()
        {       
            return Ok(new Guid());
        }
    }
}
