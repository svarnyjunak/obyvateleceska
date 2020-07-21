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
            using (var context = new CeskeObceDbContext(CreateInMemoryDbContextOptions()))
            {
                var municipalities = new List<Municipality>
                {
                    new Municipality { MunicipalityId = "1", Name = "Municipality name 1", DistrictName = "District name 1"},
                    new Municipality { MunicipalityId = "2", Name = "Municipality name 2", DistrictName = "District name 2"},
                    new Municipality { MunicipalityId = "3", Name = "Municipality name 3", DistrictName = "District name 3"},
                    new Municipality { MunicipalityId = "4", Name = "Municipality name 4", DistrictName = "District name 4"},
                    new Municipality { MunicipalityId = "5", Name = "Municipality name 5", DistrictName = "District name 5"},
                };

                context.AddRange(municipalities);
                context.SaveChanges();

                var repository = new MunicipalityRepository(context);
                var result = repository.GetRandom();
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void Exists_Test()
        {
            using (var context = new CeskeObceDbContext(CreateInMemoryDbContextOptions()))
            {
                var orders = new List<Municipality>
                {
                    new Municipality { MunicipalityId = "TEST", Name = "Municipality name", DistrictName = "District name"},
                };

                context.AddRange(orders);
                context.SaveChanges();

                var repository = new MunicipalityRepository(context);
                var result = repository.Exists(new QueryMunicipalityByCode { Code = "TEST" });
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task GetByCode_Test()
        {
            using (var dbContext = new CeskeObceDbContext(CreateInMemoryDbContextOptions()))
            {
                var municipality = new Municipality
                {
                    MunicipalityId = "TEST", 
                    Name = "Municipality name", 
                    DistrictName = "District name"
                };

                await dbContext.AddAsync(municipality);
                await dbContext.SaveChangesAsync();

                var repository = new MunicipalityRepository(dbContext);
                var result = repository.GetByCode("TEST");

                Assert.AreEqual(municipality, result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MunicipalityNotFoundException))]
        public void GetByCode_NoCodeTest()
        {
            using (var dbContext = new CeskeObceDbContext(CreateInMemoryDbContextOptions()))
            {
                var repository = new MunicipalityRepository(dbContext);
                repository.GetByCode("XXX");
            }
        }

        [TestMethod]
        public async Task ReplaceAll_Test()
        {
            using (var dbContext = new CeskeObceDbContext(CreateInMemoryDbContextOptions()))
            {
                var repository = new MunicipalityRepository(dbContext);

                var municipality = new Municipality
                {
                    MunicipalityId = "TEST",
                    Name = "Municipality name",
                    DistrictName = "District name"
                };

                await repository.ReplaceAllAsync(new[] { municipality });

                Assert.IsNotNull(await dbContext.Municipalities.FindAsync(municipality.MunicipalityId));
            }
        }

        [TestMethod]
        public async Task FindAll_Test()
        {
            using (var dbContext = new CeskeObceDbContext(CreateInMemoryDbContextOptions()))
            {
                var municipality = new Municipality
                {
                    MunicipalityId = "TEST",
                    Name = "Municipality name",
                    DistrictName = "District name"
                };

                await dbContext.Municipalities.AddAsync(municipality);
                await dbContext.SaveChangesAsync();

                var repository = new MunicipalityRepository(dbContext);
                var returned = repository.FindAll().Single();

                Assert.AreEqual(municipality, returned);
            }
        }

        [TestMethod]
        public async Task Exists_QueryTest()
        {
            using (var context = new CeskeObceDbContext(CreateInMemoryDbContextOptions()))
            {
                var municipality = new Municipality
                {
                    MunicipalityId = "TEST",
                    Name = "Municipality name",
                    DistrictName = "District name"
                };

                await context.AddAsync(municipality);
                await context.SaveChangesAsync();

                var repository = new MunicipalityRepository(context);
                var result = repository.FindAll(new QueryMunicipalityByCode { Code = "TEST" }).Single();
                Assert.AreEqual(municipality, result);
            }
        }

        private static DbContextOptions<CeskeObceDbContext> CreateInMemoryDbContextOptions()
        {
            var builder = new DbContextOptionsBuilder<CeskeObceDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;
            return options;
        }
    }
}
