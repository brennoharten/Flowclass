using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromServices] AppDbContext db)
        => Ok(new { db = "ok", time = DateTime.UtcNow });
}