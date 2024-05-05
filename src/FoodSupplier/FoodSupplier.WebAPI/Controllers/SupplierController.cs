using Microsoft.AspNetCore.Mvc;

namespace FoodSupplier.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly ILogger<SupplierController> _logger;

    public SupplierController(ILogger<SupplierController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Produce")]
    public ActionResult Produce()
    {
        _logger.LogDebug("Debug message");
        _logger.LogInformation("Info message");
        _logger.LogWarning("Warning message");

        return Ok("its alive");
    }
}