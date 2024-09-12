using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWarsAPI.Data;
using StarWarsAPI.Models.DTOs;
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
            serviceCollection.AddLogging();

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
            // It allows us to write more readable assertions
            _context.Planets.Should().BeEmpty();
            _context.Planets.Add(new Planet { IdPlanet = 1, Name = "Tatooine" });
            await _context.SaveChangesAsync();
            List<PlanetDTO> planets = await _repo.GetPlanetsAsync();
            planets.Should()
                .NotBeEmpty()
                .And.HaveCount(1);
            PlanetDTO planet = planets.FirstOrDefault();
            planet.Should().BeOfType<PlanetDTO>()
                .And.NotBeNull()
                .And.Match<PlanetDTO>(p => p.Name == "Tatooine");
            _context.Planets.Remove
                (await _context.Planets.FirstOrDefaultAsync(p => p.Name == planet.Name));
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Assert_CreatePlanetAsync()
        {
            _context.Planets.Should().BeEmpty();
            PlanetDTO planet = new PlanetDTO { Name = "Tatooine" };
            await _repo.CreatePlanetAsync(planet);
            await _context.SaveChangesAsync();
            _context.Planets.Should().HaveCount(1);
            Planet planetFromDb = await _context.Planets.FirstOrDefaultAsync(p => p.Name == "Tatooine");
            planetFromDb.Should().NotBeNull()
                .And.Match<Planet>(p => p.Name == "Tatooine");
            _context.Planets.Remove(planetFromDb);
            await _context.SaveChangesAsync();
        }
    }
}
