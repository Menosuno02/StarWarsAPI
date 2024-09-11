using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Data;
using StarWarsAPI.Models;

namespace StarWarsAPI.Repositories
{
    public class RepositorySpecies : IRepositorySpecies
    {
        private readonly StarWarsContext _context;

        public RepositorySpecies(StarWarsContext context)
        {
            _context = context;
        }

        public async Task<List<Species>> GetSpeciesAsync()
        {
            return await this._context.Species.ToListAsync();
        }

        public async Task<Species> CreateSpeciesAsync(Species species)
        {
            /*
            if (_context.Database.GetDbConnection().ConnectionString ==
                _configuration.GetConnectionString("SqlServer"))
            {
                if (!await _context.Species.AnyAsync())
                    species.IdSpecies = 1;
                else
                    species.IdSpecies = await this._context.Species.MaxAsync(s => s.IdSpecies) + 1;
            }
            */
            this._context.Add(species);
            await this._context.SaveChangesAsync();
            return species;
        }
    }
}
