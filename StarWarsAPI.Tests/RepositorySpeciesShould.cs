using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWarsAPI.Data;
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
            List<Species> species = await _repo.GetSpeciesAsync();
            Assert.NotEmpty(species);
            Assert.Single(species);
            Species speciesElement = species.FirstOrDefault();
            Assert.IsType<Species>(speciesElement);
            Assert.Equal(1, speciesElement.IdSpecies);
            Assert.Equal("Human", speciesElement.Name);
            _context.Remove(speciesElement);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_CreateSpeciesAsync()
        {
            Assert.Empty(await _repo.GetSpeciesAsync());
            Species species = new Species { Name = "Human" };
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
