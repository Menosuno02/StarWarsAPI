using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private IRepositoryPlanets _repo;

        public PlanetsController(IRepositoryPlanets repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlanetDTO>>> GetPlanets()
        {
            return await this._repo.GetPlanetsAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PlanetDTO>> CreatePlanet(PlanetDTO planet)
        {
            return await this._repo.CreatePlanetAsync(planet);
        }
    }
}
