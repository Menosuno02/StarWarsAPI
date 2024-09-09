using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsAPI.Models
{
    [Table("HABITANTS")]
    public class Habitant
    {
        [Key]
        [Column("IDHABITANT")]
        public int IdHabitant { get; set; }

        [Column("NAMEHABITANT")]
        public string Name { get; set; }

        [Column("IDSPECIES")]
        public int IdSpecies { get; set; }

        [Column("IDHOMEPLANET")]
        public int IdHomePlanet { get; set; }

        [Column("ISREBEL")]
        public bool IsRebel { get; set; }
    }
}
