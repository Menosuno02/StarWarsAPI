using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWarsAPI.Models.Entities
{
    [Table("Habitants")]
    public class Habitant
    {
        [Key]
        [Column("IdHabitant")]
        public int IdHabitant { get; set; }

        [Column("NameHabitant")]
        public string Name { get; set; }

        [Column("IdSpecies")]
        public int IdSpecies { get; set; }

        [Column("IdHomePlanet")]
        public int IdHomePlanet { get; set; }

        [Column("IsRebel")]
        public bool IsRebel { get; set; }


        public Habitant() { }

        public Habitant(int idHabitant, string name, int idSpecies, int idHomePlanet, bool isRebel)
        {
            IdHabitant = idHabitant;
            Name = name;
            IdSpecies = idSpecies;
            IdHomePlanet = idHomePlanet;
            IsRebel = isRebel;
        }
    }
}
