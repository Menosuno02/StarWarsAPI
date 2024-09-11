using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWarsAPI.Data;
using StarWarsAPI.Models.Entities;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Tests
{
    public class RepositoryHabitantsShould
    {
        private readonly IRepositoryHabitants _repo;
        private readonly StarWarsContext _context;

        public RepositoryHabitantsShould()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<StarWarsContext>(options =>
                options.UseInMemoryDatabase("StarWarsTestDatabase"));
            serviceCollection.AddTransient<IRepositoryHabitants, RepositoryHabitants>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<StarWarsContext>();
            _repo = serviceProvider.GetRequiredService<IRepositoryHabitants>();
        }

        [Fact]
        public async Task Assert_GetHabitantsAsync()
        {
            Assert.Empty(await _repo.GetHabitantsAsync());
            _context.Habitants.Add(new Habitant { IdHabitant = 2, Name = "C-3PO", IsRebel = false });
            await _context.SaveChangesAsync();
            List<Habitant> habitants = await _repo.GetHabitantsAsync();
            Assert.NotEmpty(habitants);
            Assert.Single(habitants);
            Habitant habitant = habitants.FirstOrDefault();
            Assert.IsType<Habitant>(habitant);
            Assert.Equal(2, habitant.IdHabitant);
            Assert.Equal("C-3PO", habitant.Name);
            Assert.False(habitant.IsRebel);
            _context.Remove(habitant);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_CreateHabitantAsync()
        {
            Assert.Empty(await _repo.GetHabitantsAsync());
            Habitant habitant = new Habitant { Name = "Luke Skywalker", IsRebel = true };
            await _repo.CreateHabitantAsync(habitant);
            Assert.Single(await _context.Habitants.ToListAsync());
            Habitant habitantFromDb = await _context.Habitants.FirstOrDefaultAsync(h => h.Name == "Luke Skywalker");
            Assert.NotNull(habitantFromDb);
            Assert.Equal(habitant.Name, habitantFromDb.Name);
            Assert.Equal(habitant.IsRebel, habitantFromDb.IsRebel);
            _context.Remove(habitantFromDb);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_FindHabitantAsync()
        {
            Assert.Empty(await _repo.GetHabitantsAsync());
            _context.Habitants.Add(new Habitant { IdHabitant = 3, Name = "Yoda", IsRebel = true });
            await _context.SaveChangesAsync();
            Habitant habitant = await _repo.FindHabitantAsync(3);
            Assert.NotNull(habitant);
            Assert.Equal("Yoda", habitant.Name);
            _context.Remove(habitant);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_GetRebelsAsync()
        {
            Assert.Empty(await _repo.GetRebelsAsync());
            _context.Habitants.Add(new Habitant { IdHabitant = 4, Name = "Leia Organa", IsRebel = true });
            await _context.SaveChangesAsync();
            List<Habitant> rebels = await _repo.GetRebelsAsync();
            Assert.Single(rebels);
            Assert.True(rebels.FirstOrDefault().IsRebel);
            _context.Remove(rebels.FirstOrDefault());
            await _context.SaveChangesAsync();
        }
    }
}
