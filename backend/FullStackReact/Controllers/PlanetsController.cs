using FullStackReact.DbConfig;
using FullStackReact.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FullStackReact.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanetsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public PlanetsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IActionResult GetAllPlanets()
    {
        var result = _dbContext.Planets
            .Select(x => new PlanetsListViewModel
            {
               PlanetId =  x.PlanetId,
               Name = x.Name,
               Description = x.Description,
               Type = x.Type,
               Mass = x.Mass
            });
        return Ok(result);
    }
    
}