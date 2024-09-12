using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private IRepositorySpecies _repo;
        private ILogger<SpeciesController> _logger;

        public SpeciesController
            (IRepositorySpecies repo,
            ILogger<SpeciesController> logger)
        {
            this._repo = repo;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<SpeciesDTO>>> GetSpecies()
        {
            _logger.LogInformation("Getting the species");
            return await this._repo.GetSpeciesAsync();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpeciesDTO>> CreateSpecies(SpeciesDTO species)
        {
            _logger.LogInformation("Registering new species");
            try
            {
                return await this._repo.CreateSpeciesAsync(species);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "InvalidOperationException: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
