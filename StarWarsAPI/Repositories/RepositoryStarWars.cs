using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Data;
using StarWarsAPI.Models;

namespace StarWarsAPI.Repositories
{
    public class RepositoryStarWars : IRepositoryStarWars
    {
        private StarWarsContext _context;

        public RepositoryStarWars(StarWarsContext context)
        {
            this._context = context;
        }

        #region HABITANTS
        public async Task<Habitant> CreateHabitantAsync(Habitant habitant)
        {
            if (await this._context.Habitants.CountAsync() == 0)
                habitant.IdHabitant = 1;
            else
                habitant.IdHabitant = await this._context.Habitants.MaxAsync(h => h.IdHabitant) + 1;
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
        #endregion

        #region PLANETS
        public async Task<List<Planet>> GetPlanetsAsync()
        {
            return await this._context.Planets.ToListAsync();
        }
        #endregion

        #region SPECIES 
        public async Task<List<Species>> GetSpeciesAsync()
        {
            return await this._context.Species.ToListAsync();
        }
        #endregion
    }
}
