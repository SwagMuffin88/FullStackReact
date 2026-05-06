using Microsoft.AspNetCore.Mvc;

namespace FullStackReact.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanetsController : ControllerBase
{
    public IActionResult SchoolIndex()
    {
        return Ok();
    }
}