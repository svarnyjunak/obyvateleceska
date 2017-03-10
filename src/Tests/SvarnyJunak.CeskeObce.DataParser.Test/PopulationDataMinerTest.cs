using System.Collections.Generic;
using System.Linq;
using SvarnyJunak.CeskeObce.DataParser.Entities;
using Xunit;

namespace SvarnyJunak.CeskeObce.DataParser.Tests
{
    public class PopulationDataMinerTest
    {
        private readonly PopulationDataMiner __populationDataMiner = new PopulationDataMiner();

        [Fact]
        public void ComputePopulationProgressInMunicipalities_GroupTest()
        {
            __populationDataMiner.AddPopulationData(CreateDataWithTwoFramesAndOneMunicipality());

            var result = __populationDataMiner.ComputePopulationProgressInMunicipalities();
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void ComputePopulationProgressInMunicipalities_ProgressValuesCountTest()
        {
            __populationDataMiner.AddPopulationData(CreateDataWithTwoFramesAndOneMunicipality());

            var result = __populationDataMiner.ComputePopulationProgressInMunicipalities().Single();
            Assert.Equal(2, result.PopulationProgress.Count());
        }

        [Fact]
        public void ComputePopulationProgressInMunicipalities_DoNotGroupTest()
        {
            __populationDataMiner.AddPopulationData(CreateDataWithTwoFramesAndTwoMunicipalities());

            var result = __populationDataMiner.ComputePopulationProgressInMunicipalities();
            Assert.Equal(2, result.Count());
        }

        private IEnumerable<PopulationInMunicipalitity> CreateDataWithTwoFramesAndOneMunicipality()
        {
            yield return new PopulationInMunicipalitity
            {
                MunicipalityCode = "1",
                TotalCount = 10,
                Year = 2000,
            };

            yield return new PopulationInMunicipalitity
            {
                MunicipalityCode = "1",
                TotalCount = 20,
                Year = 2001,
            };
        }

        private IEnumerable<PopulationInMunicipalitity> CreateDataWithTwoFramesAndTwoMunicipalities()
        {
            yield return new PopulationInMunicipalitity
            {
                MunicipalityCode = "1",
                TotalCount = 1,
                Year = 2000,
            };

            yield return new PopulationInMunicipalitity
            {
                MunicipalityCode = "2",
                TotalCount = 1,
                Year = 2000,
            };
        }
    }
}
