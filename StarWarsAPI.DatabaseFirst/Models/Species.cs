using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarWarsAPI.DatabaseFirst.Models;

public partial class Species
{
    [Key]
    public int IdSpecies { get; set; }

    [StringLength(100)]
    public string NameSpecies { get; set; } = null!;

    [InverseProperty("IdSpeciesNavigation")]
    public virtual ICollection<Habitant> Habitants { get; set; } = new List<Habitant>();
}
