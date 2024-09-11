using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Data;
using StarWarsAPI.Models;

namespace StarWarsAPI.Repositories
{
    public class RepositoryPlanets : IRepositoryPlanets
    {
        private readonly StarWarsContext _context;
        private readonly IConfiguration _configuration;

        public RepositoryPlanets(StarWarsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Planet>> GetPlanetsAsync()
        {
            return await this._context.Planets.ToListAsync();
        }

        public async Task<Planet> CreatePlanetAsync(Planet planet)
        {
            if (_context.Database.GetDbConnection().ConnectionString ==
                _configuration.GetConnectionString("SqlServer"))
            {
                if (!await _context.Planets.AnyAsync())
                    planet.IdPlanet = 1;
                else
                    planet.IdPlanet = await this._context.Planets.MaxAsync(p => p.IdPlanet) + 1;
            }
            this._context.Add(planet);
            await this._context.SaveChangesAsync();
            return planet;
        }
    }
}
