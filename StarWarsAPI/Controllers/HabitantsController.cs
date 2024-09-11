using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitantsController : ControllerBase
    {
        private IRepositoryHabitants _repo;

        public HabitantsController(IRepositoryHabitants repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<HabitantDTO>>> GetHabitants()
        {
            return await this._repo.GetHabitantsAsync();
        }

        [HttpGet]
        [Route("Rebels")]
        public async Task<ActionResult<List<HabitantDTO>>> GetRebels()
        {
            return await this._repo.GetRebelsAsync();
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<HabitantDTO>> FindHabitant(string name)
        {
            return await this._repo.FindHabitantAsync(name);
        }

        [HttpPost]
        public async Task<ActionResult<HabitantDTO>> InsertHabitant(HabitantDTO habitant)
        {
            return await this._repo.CreateHabitantAsync(habitant);
        }
    }
}
