using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories
{
    [TestClass]
    public class MunicipalityRepositoryTest
    {
        [TestMethod]
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
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Exists_Test()
        {
            var municipalities = new Municipality[]
            {
                new Municipality { MunicipalityId = "TEST", Name = "Municipality name", DistrictName = "District name"},
            };

            var storage = MemoryDataStorage.FromData(municipalities);
            var repository = new MunicipalityRepository(storage);
            var result = repository.Exists(new QueryMunicipalityByCode { Code = "TEST" });
            Assert.IsTrue(result);
        }

        [TestMethod]
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

            Assert.AreEqual(municipality, result);
        }

        [TestMethod]
        [ExpectedException(typeof(MunicipalityNotFoundException))]
        public void GetByCode_NoCodeTest()
        {
            var storage = MemoryDataStorage.FromData(new Municipality[0]);
            var repository = new MunicipalityRepository(storage);
            repository.GetByCode("XXX");
        }

        [TestMethod]
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

            Assert.IsNotNull(repository.GetByCode(municipality.MunicipalityId));
        }

        [TestMethod]
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

            Assert.AreEqual(municipality, returned);
        }

        [TestMethod]
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
            Assert.AreEqual(municipality, result);
        }
    }
}
