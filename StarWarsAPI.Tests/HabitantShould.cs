using StarWarsAPI.Models;

namespace StarWarsAPI.Tests
{
    public class HabitantShould
    {
        [Fact]
        public void Assert_HabitantId()
        {
            Habitant habitant = new Habitant();
            habitant.IdHabitant = 1;
            Assert.Equal(1, habitant.IdHabitant);
        }

        [Fact]
        public void Assert_HabitantName()
        {
            Habitant habitant = new Habitant();
            habitant.Name = "Luke Skywalker";
            Assert.Equal("Luke Skywalker", habitant.Name);
        }

        [Fact]
        public void Assert_HabitantSpeciesId()
        {
            Habitant habitant = new Habitant();
            habitant.IdSpecies = 1;
            Assert.Equal(1, habitant.IdSpecies);
        }

        [Fact]
        public void Assert_HabitantHomePlanetId()
        {
            Habitant habitant = new Habitant();
            habitant.IdHomePlanet = 1;
            Assert.Equal(1, habitant.IdHomePlanet);
        }

        [Fact]
        public void Assert_HabitantIsRebel()
        {
            Habitant habitant = new Habitant();
            habitant.IsRebel = true;
            Assert.True(habitant.IsRebel);
        }

        [Fact]
        public void Assert_NewHabitantIsHabitant()
        {
            Habitant habitant = new Habitant();
            Assert.IsType<Habitant>(habitant);
        }
    }
}