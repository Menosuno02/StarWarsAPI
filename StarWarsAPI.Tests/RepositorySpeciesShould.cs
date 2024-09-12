using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWarsAPI.Data;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Models.Entities;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Tests
{
    public class RepositorySpeciesShould
    {
        private readonly IRepositorySpecies _repo;
        private readonly StarWarsContext _context;

        public RepositorySpeciesShould()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<StarWarsContext>(options =>
                options.UseInMemoryDatabase("StarWarsTestDatabase"));
            serviceCollection.AddTransient<IRepositorySpecies, RepositorySpecies>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<StarWarsContext>();
            _repo = serviceProvider.GetRequiredService<IRepositorySpecies>();
        }

        [Fact]
        public async Task Assert_GetSpeciesAsync()
        {
            Assert.Empty(await _repo.GetSpeciesAsync());
            _context.Species.Add(new Species { IdSpecies = 1, Name = "Human" });
            await _context.SaveChangesAsync();
            List<SpeciesDTO> species = await _repo.GetSpeciesAsync();
            Assert.NotEmpty(species);
            Assert.Single(species);
            SpeciesDTO speciesElement = species.FirstOrDefault();
            Assert.IsType<SpeciesDTO>(speciesElement);
            Assert.Equal("Human", speciesElement.Name);
            _context.Species.Remove
                (await _context.Species.FirstOrDefaultAsync(s => s.Name == speciesElement.Name));
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_CreateSpeciesAsync()
        {
            Assert.Empty(await _repo.GetSpeciesAsync());
            SpeciesDTO species = new SpeciesDTO { Name = "Human" };
            await _repo.CreateSpeciesAsync(species);
            await _context.SaveChangesAsync();
            Assert.Single(await _context.Species.ToListAsync());
            Species speciesFromDb = await _context.Species.FirstOrDefaultAsync(s => s.Name == "Human");
            Assert.NotNull(speciesFromDb);
            Assert.Equal(species.Name, speciesFromDb.Name);
            _context.Remove(speciesFromDb);
            await _context.SaveChangesAsync();
        }
    }
}
