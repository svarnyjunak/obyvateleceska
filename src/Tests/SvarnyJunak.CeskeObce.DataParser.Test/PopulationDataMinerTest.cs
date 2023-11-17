using SvarnyJunak.CeskeObce.DataParser.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SvarnyJunak.CeskeObce.DataParser.Test
{
    public class PopulationDataMinerTest
    {
        private readonly PopulationDataMiner _populationDataMiner = new PopulationDataMiner();

        [Fact]
        public void ComputePopulationProgressInMunicipalities_GroupTest()
        {
            _populationDataMiner.AddPopulationData(CreateDataWithTwoFramesAndOneMunicipality());

            var result = _populationDataMiner.ComputePopulationProgressInMunicipalities();
            var municipalityIds = result.GroupBy(p => p.MunicipalityId);
            Assert.Single(municipalityIds);
        }

        [Fact]
        public void ComputePopulationProgressInMunicipalities_ProgressValuesCountTest()
        {
            _populationDataMiner.AddPopulationData(CreateDataWithTwoFramesAndOneMunicipality());

            var result = _populationDataMiner.ComputePopulationProgressInMunicipalities();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void ComputePopulationProgressInMunicipalities_DoNotGroupTest()
        {
            _populationDataMiner.AddPopulationData(CreateDataWithTwoFramesAndTwoMunicipalities());

            var result = _populationDataMiner.ComputePopulationProgressInMunicipalities();
            Assert.Equal(2, result.Count());
        }

        private IEnumerable<PopulationInMunicipality> CreateDataWithTwoFramesAndOneMunicipality()
        {
            yield return new PopulationInMunicipality
            {
                MunicipalityCode = "1",
                TotalCount = 10,
                Year = 2000,
            };

            yield return new PopulationInMunicipality
            {
                MunicipalityCode = "1",
                TotalCount = 20,
                Year = 2001,
            };
        }

        private IEnumerable<PopulationInMunicipality> CreateDataWithTwoFramesAndTwoMunicipalities()
        {
            yield return new PopulationInMunicipality
            {
                MunicipalityCode = "1",
                TotalCount = 1,
                Year = 2000,
            };

            yield return new PopulationInMunicipality
            {
                MunicipalityCode = "2",
                TotalCount = 1,
                Year = 2000,
            };
        }
    }
}
