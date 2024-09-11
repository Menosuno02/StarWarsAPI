using StarWarsAPI.Models;

namespace StarWarsAPI.Repositories
{
    public interface IRepositoryHabitants
    {
        public Task<List<Habitant>> GetHabitantsAsync();
        public Task<List<Habitant>> GetRebelsAsync();
        public Task<Habitant> CreateHabitantAsync(Habitant habitant);
        public Task<Habitant> FindHabitantAsync(int id);
    }
}
