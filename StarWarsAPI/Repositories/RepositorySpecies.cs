using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Data;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Models.Entities;

namespace StarWarsAPI.Repositories
{
    public class RepositorySpecies : IRepositorySpecies
    {
        private readonly StarWarsContext _context;
        private readonly ILogger<RepositorySpecies> _logger;

        public RepositorySpecies
            (StarWarsContext context,
            ILogger<RepositorySpecies> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<SpeciesDTO>> GetSpeciesAsync()
        {
            return await _context.Species
                .Select(s => new SpeciesDTO
                {
                    Name = s.Name
                })
                .ToListAsync();
        }

        public async Task<SpeciesDTO> CreateSpeciesAsync(SpeciesDTO species)
        {
            if (await DoesSpeciesExist(species.Name))
                throw new InvalidOperationException("Species already exists");
            Species speciesToCreate = new Species
            {
                Name = species.Name
            };
            if (_context.Database.IsSqlServer() &&
                !_context.Database.GetDbConnection().ConnectionString.Contains("(localdb)"))
                speciesToCreate.IdSpecies = await GenerateIdSpeciesAsync();
            _context.Species.Add(speciesToCreate);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Species registered: {species.Name}");
            return species;
        }

        private async Task<int> GenerateIdSpeciesAsync()
        {
            _logger.LogInformation("Generating the species ID");
            if (!await _context.Species.AnyAsync())
                return 1;
            return await this._context.Species
                .MaxAsync(p => p.IdSpecies) + 1;
        }

        private async Task<bool> DoesSpeciesExist(string name)
        {
            return await _context.Species
                .AnyAsync(s => s.Name == name);
        }
    }
}
