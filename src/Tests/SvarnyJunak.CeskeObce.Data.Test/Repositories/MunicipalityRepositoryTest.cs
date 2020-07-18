using System;
using System.Collections.Generic;
using System.Text;
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
                var result = repository.Exists(new QueryMunicipalityByCode{Code = "TEST"});
                Assert.IsTrue(result);
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

        private static DbContextOptions<CeskeObceDbContext> CreateInMemoryDbContextOptions()
        {
            var builder = new DbContextOptionsBuilder<CeskeObceDbContext>();
            builder.UseInMemoryDatabase("ceske-obce");
            var options = builder.Options;
            return options;
        }
    }
}
