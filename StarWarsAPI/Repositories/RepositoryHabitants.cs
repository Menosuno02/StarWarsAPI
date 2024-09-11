using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Data;
using StarWarsAPI.Models;

namespace StarWarsAPI.Repositories
{
    public class RepositoryHabitants : IRepositoryHabitants
    {
        private readonly StarWarsContext _context;
        private readonly IConfiguration _configuration;

        public RepositoryHabitants(StarWarsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Habitant> CreateHabitantAsync(Habitant habitant)
        {
            if (_context.Database.GetDbConnection().ConnectionString ==
                _configuration.GetConnectionString("SqlServer"))
            {
                if (!await _context.Habitants.AnyAsync())
                    habitant.IdHabitant = 1;
                else
                    habitant.IdHabitant = await this._context.Habitants.MaxAsync(h => h.IdHabitant) + 1;
            }
            this._context.Add(habitant);
            await this._context.SaveChangesAsync();
            return habitant;
        }

        public async Task<List<Habitant>> GetRebelsAsync()
        {
            return await this._context.Habitants
                .Where(h => h.IsRebel)
                .ToListAsync();
        }

        public async Task<List<Habitant>> GetHabitantsAsync()
        {
            return await this._context.Habitants.ToListAsync();
        }

        public async Task<Habitant> FindHabitantAsync(int id)
        {
            return await this._context.Habitants
                .FirstOrDefaultAsync(h => h.IdHabitant == id);
        }
    }
}
