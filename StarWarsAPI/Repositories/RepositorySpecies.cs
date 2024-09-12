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
            int idSpecies = await GenerateIdSpeciesAsync();
            Species speciesToCreate = new Species
            {
                IdSpecies = idSpecies,
                Name = species.Name
            };
            _context.Species.Add(speciesToCreate);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Species registered: {species.Name}");
            return species;
        }

        private async Task<int> GenerateIdSpeciesAsync()
        {
            // if (_context.Database.GetDbConnection().ConnectionString ==
            //     _configuration.GetConnectionString("SqlServer"))
            _logger.LogInformation("Generating the species ID");
            if (!await _context.Species.AnyAsync())
                return 1;
            return await this._context.Species
                .MaxAsync(p => p.IdSpecies) + 1;
        }
    }
}
