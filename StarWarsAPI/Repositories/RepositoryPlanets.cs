using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Data;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Models.Entities;

namespace StarWarsAPI.Repositories
{
    public class RepositoryPlanets : IRepositoryPlanets
    {
        private readonly StarWarsContext _context;
        private readonly ILogger<RepositoryPlanets> _logger;

        public RepositoryPlanets
            (StarWarsContext context,
            ILogger<RepositoryPlanets> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<PlanetDTO>> GetPlanetsAsync()
        {
            return await _context.Planets
                .Select(p => new PlanetDTO
                {
                    Name = p.Name
                })
                .ToListAsync();
        }

        public async Task<PlanetDTO> CreatePlanetAsync(PlanetDTO planet)
        {
            int idPlanet = await GenerateIdPlanetAsync();
            Planet planetToCreate = new Planet
            {
                IdPlanet = idPlanet,
                Name = planet.Name
            };
            _context.Planets.Add(planetToCreate);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Planet registered: {planet.Name}");
            return planet;
        }

        private async Task<int> GenerateIdPlanetAsync()
        {
            // if (_context.Database.GetDbConnection().ConnectionString ==
            //     _configuration.GetConnectionString("SqlServer"))
            _logger.LogInformation("Generating the planet ID");
            if (!await _context.Planets.AnyAsync())
                return 1;
            return await this._context.Planets
                .MaxAsync(p => p.IdPlanet) + 1;
        }
    }
}
