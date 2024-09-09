using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsAPI.Models
{
    [Table("PLANETS")]
    public class Planet
    {
        [Key]
        [Column("IDPLANET")]
        public int IdPlanet { get; set; }

        [Column("NAMEPLANET")]
        public string Name { get; set; }
    }
}
