using StarWarsAPI.Models.DTOs;

namespace StarWarsAPI.Repositories
{
    public interface IRepositoryHabitants
    {
        public Task<List<HabitantDTO>> GetHabitantsAsync();
        public Task<List<HabitantDTO>> GetRebelsAsync();
        public Task<HabitantDTO> CreateHabitantAsync(HabitantDTO habitant);
        public Task<HabitantDTO> FindHabitantAsync(string name);
    }
}
