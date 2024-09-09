using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsAPI.Models
{
    [Table("SPECIES")]
    public class Species
    {
        [Key]
        [Column("IDSPECIES")]
        public int IdSpecies { get; set; }

        [Column("NAMESPECIES")]
        public string Name { get; set; }
    }
}
