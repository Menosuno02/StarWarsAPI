using StarWarsAPI.Models;

namespace StarWarsAPI.Repositories
{
    public interface IRepositoryPlanets
    {
        public Task<List<Planet>> GetPlanetsAsync();
        public Task<Planet> CreatePlanetAsync(Planet planet);
    }
}
