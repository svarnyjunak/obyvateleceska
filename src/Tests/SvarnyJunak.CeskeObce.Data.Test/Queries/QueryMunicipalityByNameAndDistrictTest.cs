using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Data.Test.Queries
{
    [TestClass]
    public class QueryMunicipalityByNameAndDistrictTest
    {
        [TestMethod]
        public void Expression_Test()
        {
            var query = new QueryMunicipalityByNameAndDistrict
            {
                Name = "Name",
                District = "District"
            };

            var data = new[]
            {
                new Municipality
                {
                    Name = "A"
                },
                new Municipality
                {
                    Name = "Name",
                    DistrictName = "District"
                },
                new Municipality
                {
                    Name = "B"
                },
            };

            var result = data.SingleOrDefault(query.Expression.Compile());
            Assert.AreEqual("Name", result?.Name);
            Assert.AreEqual("District", result?.DistrictName);
        }
    }
}
