using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Models;

namespace StarWarsAPI.Data
{
    public class StarWarsContext : DbContext
    {
        public StarWarsContext(DbContextOptions<StarWarsContext> options)
            : base(options) { }

        public DbSet<Habitant> Habitants { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Species> Species { get; set; }
    }
}
