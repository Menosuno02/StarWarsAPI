using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Models;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private RepositoryStarWars _repo;

        public SpeciesController(RepositoryStarWars repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Species>>> GetSpecies()
        {
            return await this._repo.GetSpeciesAsync();
        }
    }
}
