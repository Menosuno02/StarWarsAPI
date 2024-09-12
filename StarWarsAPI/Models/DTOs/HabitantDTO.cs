namespace StarWarsAPI.Models.DTOs
{
    public class HabitantDTO : INameable
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string HomePlanet { get; set; }
        public bool IsRebel { get; set; }
    }
}
