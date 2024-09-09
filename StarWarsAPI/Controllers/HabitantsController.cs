using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Models;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitantsController : ControllerBase
    {
        private RepositoryStarWars _repo;

        public HabitantsController(RepositoryStarWars repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Habitant>>> GetHabitants()
        {
            return await this._repo.GetHabitantsAsync();
        }

        [HttpGet]
        [Route("Rebels")]
        public async Task<ActionResult<List<Habitant>>> GetRebels()
        {
            return await this._repo.GetRebelsAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Habitant>> FindHabitant(int id)
        {
            return await this._repo.FindHabitantAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Habitant>> InsertHabitant(Habitant habitant)
        {
            return await this._repo.CreateHabitantAsync(habitant);
        }
    }
}
