using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsAPI.Models.Entities
{
    [Table("Species")]
    public class Species
    {
        [Key]
        [Column("IdSpecies")]
        public int IdSpecies { get; set; }

        [Column("NameSpecies")]
        public string Name { get; set; }
    }
}
