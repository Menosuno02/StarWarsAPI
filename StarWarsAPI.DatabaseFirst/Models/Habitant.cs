using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarWarsAPI.DatabaseFirst.Models;

public partial class Habitant
{
    [Key]
    public int IdHabitant { get; set; }

    [StringLength(100)]
    public string NameHabitant { get; set; } = null!;

    public int IdSpecies { get; set; }

    public int IdHomePlanet { get; set; }

    public bool IsRebel { get; set; }

    [ForeignKey("IdHomePlanet")]
    [InverseProperty("Habitants")]
    public virtual Planet IdHomePlanetNavigation { get; set; } = null!;

    [ForeignKey("IdSpecies")]
    [InverseProperty("Habitants")]
    public virtual Species IdSpeciesNavigation { get; set; } = null!;
}
