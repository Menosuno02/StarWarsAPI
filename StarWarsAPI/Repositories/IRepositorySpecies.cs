using StarWarsAPI.Models;

namespace StarWarsAPI.Repositories
{
    public interface IRepositorySpecies
    {
        public Task<List<Species>> GetSpeciesAsync();
        public Task<Species> CreateSpeciesAsync(Species species);
    }
}
