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
        private ILogger<PlanetsController> _logger;

        public PlanetsController
            (IRepositoryPlanets repo,
            ILogger<PlanetsController> logger)
        {
            this._repo = repo;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlanetDTO>>> GetPlanets()
        {
            _logger.LogInformation("Getting the planets");
            return await this._repo.GetPlanetsAsync();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlanetDTO>> CreatePlanet(PlanetDTO planet)
        {
            _logger.LogInformation("Registering new planet");
            try
            {
                return await this._repo.CreatePlanetAsync(planet);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "InvalidOperationException: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
