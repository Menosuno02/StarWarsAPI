using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Models;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private IRepositorySpecies _repo;

        public SpeciesController(IRepositorySpecies repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Species>>> GetSpecies()
        {
            return await this._repo.GetSpeciesAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Species>> CreateSpecies(Species species)
        {
            return await this._repo.CreateSpeciesAsync(species);
        }
    }
}
