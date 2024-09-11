using StarWarsAPI.Models.DTOs;

namespace StarWarsAPI.Repositories
{
    public interface IRepositorySpecies
    {
        public Task<List<SpeciesDTO>> GetSpeciesAsync();
        public Task<SpeciesDTO> CreateSpeciesAsync(SpeciesDTO species);
    }
}
