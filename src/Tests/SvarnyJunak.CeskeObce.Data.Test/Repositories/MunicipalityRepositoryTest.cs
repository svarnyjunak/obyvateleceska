using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories
{
    public class MunicipalityRepositoryTest
    {
        [Fact]
        public void GetRandom_Test()
        {
            var municipalities = new[]
            {
                new Municipality { MunicipalityId = "1", Name = "Municipality name 1", DistrictName = "District name 1"},
                new Municipality { MunicipalityId = "2", Name = "Municipality name 2", DistrictName = "District name 2"},
                new Municipality { MunicipalityId = "3", Name = "Municipality name 3", DistrictName = "District name 3"},
                new Municipality { MunicipalityId = "4", Name = "Municipality name 4", DistrictName = "District name 4"},
                new Municipality { MunicipalityId = "5", Name = "Municipality name 5", DistrictName = "District name 5"},
            };

            var storage = MemoryDataStorage.FromData(municipalities);
            var repository = new MunicipalityRepository(storage);
            var result = repository.GetRandom();
            Assert.NotNull(result);
        }

        [Fact]
        public void Exists_Test()
        {
            var municipalities = new Municipality[]
            {
                new Municipality { MunicipalityId = "TEST", Name = "Municipality name", DistrictName = "District name"},
            };

            var storage = MemoryDataStorage.FromData(municipalities);
            var repository = new MunicipalityRepository(storage);
            var result = repository.Exists(new QueryMunicipalityByCode { Code = "TEST" });
            Assert.True(result);
        }

        [Fact]
        public void GetByCode_Test()
        {
            var municipality = new Municipality
            {
                MunicipalityId = "TEST",
                Name = "Municipality name",
                DistrictName = "District name"
            };

            var storage = MemoryDataStorage.FromData(municipality);
            var repository = new MunicipalityRepository(storage);
            var result = repository.GetByCode("TEST");

            Assert.Equal(municipality, result);
        }

        [Fact]
        public void GetByCode_NoCodeTest()
        {
            var storage = MemoryDataStorage.FromData(new Municipality[0]);
            var repository = new MunicipalityRepository(storage);

            Assert.Throws<MunicipalityNotFoundException>(() => repository.GetByCode("XXX"));
        }

        [Fact]
        public async Task ReplaceAll_Test()
        {
            var municipality = new Municipality
            {
                MunicipalityId = "TEST",
                Name = "Municipality name",
                DistrictName = "District name"
            };

            var storage = MemoryDataStorage.FromData(municipality);
            var repository = new MunicipalityRepository(storage);

            await repository.ReplaceAllAsync(new[] { municipality });

            Assert.NotNull(repository.GetByCode(municipality.MunicipalityId));
        }

        [Fact]
        public void FindAll_Test()
        {
            var municipality = new Municipality
            {
                MunicipalityId = "TEST",
                Name = "Municipality name",
                DistrictName = "District name"
            };

            var storage = MemoryDataStorage.FromData(municipality);
            var repository = new MunicipalityRepository(storage);
            var returned = repository.FindAll().Single();

            Assert.Equal(municipality, returned);
        }

        [Fact]
        public void Exists_QueryTest()
        {
            var municipality = new Municipality
            {
                MunicipalityId = "TEST",
                Name = "Municipality name",
                DistrictName = "District name"
            };

            var storage = MemoryDataStorage.FromData(municipality);
            var repository = new MunicipalityRepository(storage);
            var result = repository.FindAll(new QueryMunicipalityByCode { Code = "TEST" }).Single();
            Assert.Equal(municipality, result);
        }
    }
}
