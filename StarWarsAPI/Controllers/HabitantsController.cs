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
        private ILogger<HabitantsController> _logger;

        public HabitantsController
            (IRepositoryHabitants repo,
            ILogger<HabitantsController> logger)
        {
            this._repo = repo;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<HabitantDTO>>> GetHabitants()
        {
            _logger.LogInformation("Getting the habitants");
            return await this._repo.GetHabitantsAsync();
        }

        [HttpGet]
        [Route("Rebels")]
        public async Task<ActionResult<List<HabitantDTO>>> GetRebels()
        {
            _logger.LogInformation("Getting the rebels");
            return await this._repo.GetRebelsAsync();
        }

        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HabitantDTO>> FindHabitant(string name)
        {
            _logger.LogInformation($"Searching the habitant: {name}");
            try
            {
                return await this._repo.FindHabitantAsync(name);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "KeyNotFoundException: {Message}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<HabitantDTO>> InsertHabitant(HabitantDTO habitant)
        {
            _logger.LogInformation("Registering new habitant");
            return await this._repo.CreateHabitantAsync(habitant);
        }
    }
}
