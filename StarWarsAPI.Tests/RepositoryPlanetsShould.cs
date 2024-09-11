using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWarsAPI.Data;
using FluentAssertions;
using StarWarsAPI.Models.Entities;
using StarWarsAPI.Repositories;

namespace StarWarsAPI.Tests
{
    public class RepositoryPlanetsShould
    {
        private readonly IRepositoryPlanets _repo;
        private readonly StarWarsContext _context;

        public RepositoryPlanetsShould()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<StarWarsContext>(options =>
                options.UseInMemoryDatabase("StarWarsTestDatabase"));
            serviceCollection.AddTransient<IRepositoryPlanets, RepositoryPlanets>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<StarWarsContext>();
            _repo = serviceProvider.GetRequiredService<IRepositoryPlanets>();
        }

        [Fact]
        public async Task Assert_GetPlanetsAsync()
        {
            // Install the NuGet package FluentAssertions
            // It's a package that allows us to write more readable assertions
            _context.Planets.Should().BeEmpty();
            _context.Planets.Add(new Planet { IdPlanet = 1, Name = "Tatooine" });
            await _context.SaveChangesAsync();
            List<Planet> planets = await _repo.GetPlanetsAsync();
            planets.Should()
                .NotBeEmpty()
                .And.HaveCount(1);
            Planet planet = planets.FirstOrDefault();
            planet.Should().BeOfType<Planet>()
                .And.NotBeNull()
                .And.Match<Planet>(p => p.IdPlanet == 1 && p.Name == "Tatooine");
            _context.Remove(planet);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_CreatePlanetAsync()
        {
            _context.Planets.Should().BeEmpty();
            Planet planet = new Planet { Name = "Tatooine" };
            await _repo.CreatePlanetAsync(planet);
            await _context.SaveChangesAsync();
            _context.Planets.Should().HaveCount(1);
            Planet planetFromDb = await _context.Planets.FirstOrDefaultAsync(p => p.Name == "Tatooine");
            planetFromDb.Should().NotBeNull()
                .And.Match<Planet>(p => p.Name == "Tatooine");
            _context.Remove(planetFromDb);
            await _context.SaveChangesAsync();
        }
    }
}
