using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories
{
    [TestClass]
    public class PopulationFrameRepositoryTest
    {
        [TestMethod]
        public void Exists_Test()
        {
            using (var context = new CeskeObceDbContext(CreateInMemoryDbContextOptions()))
            {
                var municipality = new Municipality
                {
                    MunicipalityId = "TEST1",
                    Name = "Municipality name",
                    DistrictName = "District name"
                };

                context.Add(municipality);

                var data = new List<PopulationFrame>
                {
                    new PopulationFrame
                    {
                        Count = 1,
                        MunicipalityId = municipality.MunicipalityId, 
                        Year = 2000
                    }
                };

                context.AddRange(data);
                context.SaveChanges();

                var repository = new PopulationFrameRepository(context);
                var result = repository.Exists(new QueryPopulationFrameByMunicipalityCode
                {
                    Code = municipality.MunicipalityId
                });

                Assert.IsTrue(result);
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
