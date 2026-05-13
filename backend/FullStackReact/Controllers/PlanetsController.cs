using FullStackReact.DbConfig;
using FullStackReact.Entities;
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
    
    [HttpGet]
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

    [HttpPost("new")]
    public async Task<IActionResult> AddNewPlanet([FromBody] PlanetsListViewModel planet)
    {
        try
        {
            var newPlanet = new Planet
            {
                Name = planet.Name,
                Description = planet.Description,
                Type = planet.Type,
                Mass = planet.Mass
            };

            _dbContext.Add(newPlanet);
            await _dbContext.SaveChangesAsync();

            return Ok(new {message = "Adding planet was successful!"});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return BadRequest("Could not save planet to db: "  + e.Message);
        }
    }
}