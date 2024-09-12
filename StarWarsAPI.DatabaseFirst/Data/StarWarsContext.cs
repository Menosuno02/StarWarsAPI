using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StarWarsAPI.DatabaseFirst.Models;

public partial class StarWarsContext : DbContext
{
    public StarWarsContext()
    {
    }

    public StarWarsContext(DbContextOptions<StarWarsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Habitant> Habitants { get; set; }

    public virtual DbSet<Planet> Planets { get; set; }

    public virtual DbSet<Species> Species { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=encamina;Persist Security Info=True;User ID=sa;Password=12345;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habitant>(entity =>
        {
            entity.HasKey(e => e.IdHabitant).HasName("PK__HABITANT__55F90CC4DE12D24B");

            entity.Property(e => e.IdHabitant).ValueGeneratedNever();

            entity.HasOne(d => d.IdHomePlanetNavigation).WithMany(p => p.Habitants).HasConstraintName("FK_HOMEPLANET");

            entity.HasOne(d => d.IdSpeciesNavigation).WithMany(p => p.Habitants).HasConstraintName("FK_SPECIES");
        });

        modelBuilder.Entity<Planet>(entity =>
        {
            entity.HasKey(e => e.IdPlanet).HasName("PK__PLANETS__E3B1A2FA045925FF");

            entity.Property(e => e.IdPlanet).ValueGeneratedNever();
        });

        modelBuilder.Entity<Species>(entity =>
        {
            entity.HasKey(e => e.IdSpecies).HasName("PK__SPECIES__6EEDF29973EC2EA6");

            entity.Property(e => e.IdSpecies).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
