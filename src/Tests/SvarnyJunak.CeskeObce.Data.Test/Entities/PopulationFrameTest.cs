using SvarnyJunak.CeskeObce.Data.Entities;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Entities
{
    public class PopulationFrameTest
    {
        [Fact]
        public void Year_Test()
        {
            var populationFrame = new PopulationFrame
            {
                Year = 2000
            };

            Assert.Equal(2000, populationFrame.Year);
        }

        [Fact]
        public void Count_Test()
        {
            var populationFrame = new PopulationFrame
            {
                Count = 11
            };
            
            Assert.Equal(11, populationFrame.Count);
        }
    }
}
