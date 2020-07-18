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
    public class QueryMunicipalityByCodeTest
    {
        [TestMethod]
        public void CodeProperty_Test()
        {
            var query = new QueryMunicipalityByCode { Code = "XXX" };
            Assert.AreEqual("XXX", query.Code);
        }

        [TestMethod]
        public void Expression_Test()
        {
            var query = new QueryMunicipalityByCode { Code = "XXX" };

            var data = new[]
            {
                new Municipality
                {
                    MunicipalityId = "A"
                },
                new Municipality
                {
                    MunicipalityId = "XXX",
                },
                new Municipality
                {
                    MunicipalityId = "B"
                },
            };

            var result = data.SingleOrDefault(query.Expression.Compile());
            Assert.AreEqual("XXX", result?.MunicipalityId);
        }
    }
}
