using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Models;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private IRepositoryStarWars _repo;

        public PlanetsController(IRepositoryStarWars repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Planet>>> GetPlanets()
        {
            return await this._repo.GetPlanetsAsync();
        }
    }
}
