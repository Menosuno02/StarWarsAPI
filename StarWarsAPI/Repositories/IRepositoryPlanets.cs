using StarWarsAPI.Models.DTOs;

namespace StarWarsAPI.Repositories
{
    public interface IRepositoryPlanets
    {
        public Task<List<PlanetDTO>> GetPlanetsAsync();
        public Task<PlanetDTO> CreatePlanetAsync(PlanetDTO planet);
    }
}
