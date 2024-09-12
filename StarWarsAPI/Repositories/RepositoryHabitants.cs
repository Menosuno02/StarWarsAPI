using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StarWarsAPI.Data;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Models.Entities;

namespace StarWarsAPI.Repositories
{
    public class RepositoryHabitants : IRepositoryHabitants
    {
        private readonly StarWarsContext _context;
        private readonly IRepositoryPlanets _repositoryPlanets;
        private readonly IRepositorySpecies _repositorySpecies;
        private readonly ILogger<RepositoryHabitants> _logger;

        public RepositoryHabitants
            (StarWarsContext context,
            IRepositoryPlanets repositoryPlanets,
            IRepositorySpecies repositorySpecies,
            ILogger<RepositoryHabitants> logger)
        {
            _context = context;
            _repositoryPlanets = repositoryPlanets;
            _repositorySpecies = repositorySpecies;
            _logger = logger;
        }

        public async Task<List<HabitantDTO>> GetHabitantsAsync()
        {
            return await (from h in _context.Habitants
                          join p in _context.Planets on h.IdHomePlanet equals p.IdPlanet
                          join s in _context.Species on h.IdSpecies equals s.IdSpecies
                          select new HabitantDTO
                          {
                              Name = h.Name,
                              IsRebel = h.IsRebel,
                              HomePlanet = p.Name,
                              Species = s.Name
                          })
                         .ToListAsync();
        }

        public async Task<HabitantDTO> CreateHabitantAsync(HabitantDTO habitant)
        {
            Habitant habitantToCreate = await GenerateHabitantAsync(habitant);
            _context.Habitants.Add(habitantToCreate);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Habitant registered: {habitant.Name}");
            return habitant;
        }

        public async Task<List<HabitantDTO>> GetRebelsAsync()
        {
            List<HabitantDTO> habitants = await GetHabitantsAsync();
            return habitants
                .Where(h => h.IsRebel)
                .ToList();
        }

        public async Task<HabitantDTO> FindHabitantAsync(string name)
        {
            Habitant habitant = await _context.Habitants
                .Where(h => h.Name == name)
                .FirstOrDefaultAsync();
            if (habitant == null)
                throw new KeyNotFoundException
                    ($"Habitant not found with name {name}");

            string homePlanet = await GetNamePlanetAsync(habitant.IdHomePlanet);
            string species = await GetNameSpeciesAsync(habitant.IdSpecies);
            HabitantDTO habitantDTO = new HabitantDTO
            {
                Name = habitant.Name,
                IsRebel = habitant.IsRebel,
                HomePlanet = homePlanet,
                Species = species
            };
            return habitantDTO;
        }

        private async Task<int> GetIdHomePlanetAsync(string name)
        {
            _logger.LogInformation($"Getting the planet ID of {name}");
            int count = await this._context.Planets
                .CountAsync(p => p.Name == name);
            if (count == 0)
            {
                _logger.LogWarning($"Planet not found: {name}. Registering it");
                await _repositoryPlanets
                    .CreatePlanetAsync(new PlanetDTO { Name = name });
            }
            int idHomePlanet = await this._context.Planets
                    .Where(p => p.Name == name)
                    .Select(p => p.IdPlanet)
                    .FirstOrDefaultAsync();
            return idHomePlanet;
        }

        private async Task<int> GetIdSpeciesAsync(string name)
        {
            _logger.LogInformation($"Getting the species ID of {name}");
            int count = await this._context.Species
                .CountAsync(s => s.Name == name);
            if (count == 0)
            {
                _logger.LogWarning($"Species not found: {name}. Registering it");
                await _repositorySpecies
                    .CreateSpeciesAsync(new SpeciesDTO { Name = name });
            }
            int idSpecies = await _context.Species
                .Where(s => s.Name == name)
                .Select(s => s.IdSpecies)
                .FirstOrDefaultAsync();
            return idSpecies;
        }

        private async Task<string> GetNamePlanetAsync(int id)
        {
            return await _context.Planets
                .Where(p => p.IdPlanet == id)
                .Select(p => p.Name)
                .FirstOrDefaultAsync();
        }

        private async Task<string> GetNameSpeciesAsync(int id)
        {
            return await _context.Species
                .Where(s => s.IdSpecies == id)
                .Select(s => s.Name)
                .FirstOrDefaultAsync();
        }

        private async Task<int> GenerateIdHabitantAsync()
        {
            // if (_context.Database.GetDbConnection().ConnectionString ==
            //     _configuration.GetConnectionString("SqlServer"))
            _logger.LogInformation("Generating the habitant ID");
            if (!await _context.Habitants.AnyAsync())
                return 1;
            return await this._context.Habitants
                .MaxAsync(h => h.IdHabitant) + 1;
        }

        private async Task<Habitant> GenerateHabitantAsync(HabitantDTO habitant)
        {
            int idHabitant = await GenerateIdHabitantAsync();
            int idHomePlanet = await GetIdHomePlanetAsync(habitant.HomePlanet);
            int idSpecies = await GetIdSpeciesAsync(habitant.Species);
            Habitant habitantToCreate = new Habitant
            {
                IdHabitant = idHabitant,
                Name = habitant.Name,
                IsRebel = habitant.IsRebel,
                IdHomePlanet = idHomePlanet,
                IdSpecies = idSpecies
            };
            return habitantToCreate;
        }
    }
}
