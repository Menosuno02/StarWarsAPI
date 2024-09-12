using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWarsAPI.Data;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Models.Entities;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Tests
{
    public class RepositoryHabitantsShould
    {
        private readonly IRepositoryHabitants _repoHabitants;
        private readonly IRepositoryPlanets _repoPlanets;
        private readonly IRepositorySpecies _repoSpecies;
        private readonly StarWarsContext _context;

        public RepositoryHabitantsShould()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<StarWarsContext>(options =>
                options.UseInMemoryDatabase("StarWarsTestDatabase"));
            serviceCollection.AddTransient<IRepositoryHabitants, RepositoryHabitants>();
            serviceCollection.AddTransient<IRepositoryPlanets, RepositoryPlanets>();
            serviceCollection.AddTransient<IRepositorySpecies, RepositorySpecies>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<StarWarsContext>();
            _repoHabitants = serviceProvider.GetRequiredService<IRepositoryHabitants>();
            _repoPlanets = serviceProvider.GetRequiredService<IRepositoryPlanets>();
            _repoSpecies = serviceProvider.GetRequiredService<IRepositorySpecies>();
        }

        [Fact]
        public async Task Assert_GetHabitantsAsync()
        {
            Assert.Empty(await _repoHabitants.GetHabitantsAsync());
            HabitantDTO habitant = new HabitantDTO
            { Name = "Luke Skywalker", IsRebel = false, HomePlanet = "Bespin", Species = "Human" };
            await _repoHabitants.CreateHabitantAsync(habitant);
            List<HabitantDTO> habitants = await _repoHabitants.GetHabitantsAsync();
            Assert.NotEmpty(habitants);
            Assert.Single(habitants);
            HabitantDTO habitantRepo = habitants.FirstOrDefault();
            Assert.IsType<HabitantDTO>(habitantRepo);
            Assert.Equal("Luke Skywalker", habitantRepo.Name);
            Assert.False(habitantRepo.IsRebel);
            _context.Habitants.Remove
                (await _context.Habitants.FirstOrDefaultAsync(h => h.Name == habitantRepo.Name));
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_CreateHabitantAsync()
        {
            Assert.Empty(await _repoHabitants.GetHabitantsAsync());
            HabitantDTO habitant = new HabitantDTO
            { Name = "Luke Skywalker", IsRebel = true, HomePlanet = "Bespin", Species = "Human" };
            await _repoHabitants.CreateHabitantAsync(habitant);
            Assert.Single(await _context.Habitants.ToListAsync());
            Habitant habitantFromDb = await _context.Habitants.FirstOrDefaultAsync(h => h.Name == "Luke Skywalker");
            Assert.NotNull(habitantFromDb);
            Assert.Equal(habitant.Name, habitantFromDb.Name);
            Assert.Equal(habitant.IsRebel, habitantFromDb.IsRebel);
            _context.Habitants.Remove(habitantFromDb);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_FindHabitantAsync()
        {
            Assert.Empty(await _repoHabitants.GetHabitantsAsync());
            HabitantDTO habitant = new HabitantDTO
            { Name = "Luke Skywalker", IsRebel = true, HomePlanet = "Bespin", Species = "Human" };
            await _repoHabitants.CreateHabitantAsync(habitant);
            HabitantDTO habitantDTO = await _repoHabitants.FindHabitantAsync("Luke Skywalker");
            Assert.NotNull(habitantDTO);
            Assert.Equal("Luke Skywalker", habitantDTO.Name);
            Assert.True(habitantDTO.IsRebel);
            Assert.Equal("Bespin", habitantDTO.HomePlanet);
            Habitant habitantFromDb = await _context.Habitants
                .FirstOrDefaultAsync(h => h.Name == habitantDTO.Name);
            _context.Habitants.Remove(habitantFromDb);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_GetRebelsAsync()
        {
            Assert.Empty(await _repoHabitants.GetRebelsAsync());
            await _context.Habitants.AddAsync(new Habitant
            { Name = "Leia Organa", IsRebel = true, IdHomePlanet = 1, IdSpecies = 1 });
            await _context.SaveChangesAsync();
            List<HabitantDTO> rebels = await _repoHabitants.GetRebelsAsync();
            Assert.NotEmpty(rebels);
            Assert.Single(rebels);
            HabitantDTO habitantRepo = rebels.FirstOrDefault();
            Assert.IsType<HabitantDTO>(habitantRepo);
            Assert.Equal("Leia Organa", habitantRepo.Name);
            Assert.True(habitantRepo.IsRebel);
            _context.Habitants.Remove
                (await _context.Habitants.FirstOrDefaultAsync(h => h.Name == habitantRepo.Name));
            await _context.SaveChangesAsync();
        }
    }
}
