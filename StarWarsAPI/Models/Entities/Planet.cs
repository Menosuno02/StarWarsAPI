using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsAPI.Models.Entities
{
    [Table("Planets")]
    public class Planet
    {
        [Key]
        [Column("IdPlanet")]
        public int IdPlanet { get; set; }

        [Column("NamePlanet")]
        public string Name { get; set; }
    }
}
