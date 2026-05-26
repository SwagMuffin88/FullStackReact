using FullStackReact.DbConfig;
using FullStackReact.Entities;
using FullStackReact.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            .Select(x => new PlanetViewModel
            {
               PlanetId =  x.PlanetId,
               Name = x.Name,
               Description = x.Description,
               Type = x.Type,
               Mass = x.Mass
            });
        return Ok(result);
    }
    
    private async Task<Planet?> GetPlanet(Guid planetId)
    {
        return await _dbContext.Planets
            .FirstOrDefaultAsync(p => p.PlanetId == planetId);
    }

    private async Task<PlanetViewModel?> GetPlanetAsViewModel(Guid planetId)
    {
        var planet = await GetPlanet(planetId);
        
        return new PlanetViewModel
        {
            PlanetId = planet.PlanetId,
            Name = planet.Name,
            Description = planet.Description,
            Type = planet.Type,
            Mass = planet.Mass
        };
    }

    [HttpPost("new")]
    public async Task<IActionResult> AddNewPlanet([FromBody] PlanetViewModel planet)
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
    
    [HttpGet("{planetId:guid}")]
    public async Task<IActionResult> GetPlanetDetails(Guid planetId)
    {
        try
        {
            var planetViewModel = await GetPlanetAsViewModel(planetId);
            
            if (planetViewModel == null)
            {
                return NotFound("Could not find planet with id " + planetId);
            }
            return Ok(planetViewModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return BadRequest("Could not get planet with id " + planetId + " from db: "  + e.Message);
        }
    }
    
    [HttpPut("{planetId:guid}")]
    public async Task<IActionResult> Update(Guid planetId, [FromBody] PlanetViewModel model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Name is required");
            }
            
            var planet = await GetPlanet(planetId);
            if (planet == null) return NotFound();

            planet.Name = model.Name;
            planet.Description = model.Description;
            planet.Type = model.Type;
            planet.Mass = model.Mass;

            await _dbContext.SaveChangesAsync();
            return Ok(new { message = "Updating planet was successful!" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return BadRequest("Could not update planet in db: " + e.Message);
        }
    }
}