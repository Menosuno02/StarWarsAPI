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
            if (await DoesPlanetExist(planet.Name))
                throw new InvalidOperationException("Planet already exists");
            Planet planetToCreate = new Planet
            {
                Name = planet.Name
            };
            if (_context.Database.IsSqlServer() &&
                !_context.Database.GetDbConnection().ConnectionString.Contains("(localdb)"))
                planetToCreate.IdPlanet = await GenerateIdPlanetAsync();
            _context.Planets.Add(planetToCreate);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Planet registered: {planet.Name}");
            return planet;
        }

        private async Task<int> GenerateIdPlanetAsync()
        {
            _logger.LogInformation("Generating the planet ID");
            if (!await _context.Planets.AnyAsync())
                return 1;
            return await this._context.Planets
                .MaxAsync(p => p.IdPlanet) + 1;
        }

        private async Task<bool> DoesPlanetExist(string name)
        {
            return await _context.Planets
                .AnyAsync(p => p.Name == name);
        }
    }
}
