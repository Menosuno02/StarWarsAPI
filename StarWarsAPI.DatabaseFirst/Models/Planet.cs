using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarWarsAPI.DatabaseFirst.Models;

public partial class Planet
{
    [Key]
    public int IdPlanet { get; set; }

    [StringLength(100)]
    public string NamePlanet { get; set; } = null!;

    [InverseProperty("IdHomePlanetNavigation")]
    public virtual ICollection<Habitant> Habitants { get; set; } = new List<Habitant>();
}
