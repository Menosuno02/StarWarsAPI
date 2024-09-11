using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Data;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Models.Entities;

namespace StarWarsAPI.Repositories
{
    public class RepositoryPlanets : IRepositoryPlanets
    {
        private readonly StarWarsContext _context;

        public RepositoryPlanets(StarWarsContext context)
        {
            _context = context;
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
            return planet;
        }

        private async Task<int> GenerateIdPlanetAsync()
        {
            // if (_context.Database.GetDbConnection().ConnectionString ==
            //     _configuration.GetConnectionString("SqlServer"))
            if (!await _context.Planets.AnyAsync())
                return 1;
            return await this._context.Planets
                .MaxAsync(p => p.IdPlanet) + 1;
        }
    }
}
