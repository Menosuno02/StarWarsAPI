using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Data;
using StarWarsAPI.Models;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Tests
{
    public class RepositoryShould
    {
        private readonly RepositoryStarWars _repo;
        private readonly StarWarsContext _context;

        public RepositoryShould()
        {
            _context = new StarWarsContext(new DbContextOptionsBuilder<StarWarsContext>()
                .UseInMemoryDatabase(databaseName: "StarWarsTestDatabase")
                .Options);
            _repo = new RepositoryStarWars(_context);
        }

        [Fact]
        public async Task Assert_GetHabitantsAsync()
        {
            _context.Habitants.Add(new Habitant { IdHabitant = 2, Name = "C-3PO", IsRebel = false });
            await _context.SaveChangesAsync();
            List<Habitant> habitants = await _repo.GetHabitantsAsync();
            Assert.NotEmpty(habitants);
            Assert.IsType<Habitant>(habitants.FirstOrDefault());
        }

        [Fact]
        public async Task Assert_GetSpeciesAsync()
        {
            _context.Species.Add(new Species { IdSpecies = 1, Name = "Human" });
            await _context.SaveChangesAsync();
            List<Species> species = await _repo.GetSpeciesAsync();
            Assert.NotEmpty(species);
            Assert.IsType<Species>(species.FirstOrDefault());
        }

        [Fact]
        public async Task Assert_GetPlanetsAsync()
        {
            _context.Planets.AddRange(new Planet { IdPlanet = 1, Name = "Tatooine" });
            await _context.SaveChangesAsync();
            List<Planet> planets = await _repo.GetPlanetsAsync();
            Assert.NotEmpty(planets);
            Assert.IsType<Planet>(planets.FirstOrDefault());
        }

        [Fact]
        public async Task Assert_CreateHabitantAsync()
        {
            Habitant habitant = new Habitant { Name = "Luke Skywalker", IsRebel = true };
            await _repo.CreateHabitantAsync(habitant);
            Habitant habitantFromDb = await _context.Habitants.FirstOrDefaultAsync(h => h.Name == "Luke Skywalker");
            Assert.NotNull(habitantFromDb);
            Assert.Equal(habitant.Name, habitantFromDb.Name);
        }


        [Fact]
        public async Task Assert_FindHabitantAsync()
        {
            _context.Habitants.Add(new Habitant { IdHabitant = 3, Name = "Yoda", IsRebel = true });
            await _context.SaveChangesAsync();
            Habitant habitant = await _repo.FindHabitantAsync(3);
            Assert.NotNull(habitant);
            Assert.Equal("Yoda", habitant.Name);
        }

        [Fact]
        public async Task Assert_GetRebelsAsync()
        {
            _context.Habitants.Add(new Habitant { IdHabitant = 4, Name = "Leia Organa", IsRebel = true });
            await _context.SaveChangesAsync();
            List<Habitant> rebels = await _repo.GetRebelsAsync();
            Assert.NotEmpty(rebels);
            Assert.True(rebels.FirstOrDefault().IsRebel);
        }
    }
}
