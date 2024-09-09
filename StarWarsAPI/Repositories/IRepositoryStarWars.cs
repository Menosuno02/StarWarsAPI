using StarWarsAPI.Models;

namespace StarWarsAPI.Repositories
{
    public interface IRepositoryStarWars
    {
        #region HABITANTS
        public Task<List<Habitant>> GetHabitantsAsync();
        public Task<List<Habitant>> GetRebelsAsync();
        public Task<Habitant> CreateHabitantAsync(Habitant habitant);
        public Task<Habitant> FindHabitantAsync(int id);
        #endregion

        #region PLANETS
        public Task<List<Planet>> GetPlanetsAsync();
        #endregion

        #region SPECIES
        public Task<List<Species>> GetSpeciesAsync();
        #endregion
    }
}
