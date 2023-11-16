using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories
{
    public class PopulationFrameRepositoryTest
    {
        [Fact]
        public void Exists_Test()
        {
            var municipality = new Municipality
            {
                MunicipalityId = "TEST1",
                Name = "Municipality name",
                DistrictName = "District name"
            };

            var frame = new PopulationFrame
            {
                Count = 1,
                MunicipalityId = municipality.MunicipalityId,
                Year = 2000
            };

            var populationFrameDataStorage = MemoryDataStorage.FromData(frame);
            var repository = new PopulationFrameRepository(populationFrameDataStorage);
            var result = repository.Exists(new QueryPopulationFrameByMunicipalityCode
            {
                Code = municipality.MunicipalityId
            });

            Assert.True(result);
        }
    }
}
