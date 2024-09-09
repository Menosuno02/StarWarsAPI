using StarWarsAPI.Models;

namespace StarWarsAPI.Tests
{
    public class HabitantShould
    {
        private readonly Habitant _habitant;

        public HabitantShould()
        {
            this._habitant = new Habitant(1, "Luke Skywalker", 1, 1, false);
        }

        [Fact]
        public void Assert_HabitantId()
        {
            Assert.Equal(1, this._habitant.IdHabitant);
        }

        [Fact]
        public void Assert_HabitantName()
        {
            Assert.Equal("Luke Skywalker", this._habitant.Name);
        }

        [Fact]
        public void Assert_HabitantSpeciesId()
        {
            Assert.Equal(1, this._habitant.IdSpecies);
        }

        [Fact]
        public void Assert_HabitantHomePlanetId()
        {
            Assert.Equal(1, this._habitant.IdHomePlanet);
        }

        [Fact]
        public void Assert_HabitantIsRebel()
        {
            this._habitant.IsRebel = true;
            Assert.True(this._habitant.IsRebel);
        }

        [Fact]
        public void Assert_NewHabitantIsHabitant()
        {
            Assert.IsType<Habitant>(this._habitant);
        }
    }
}