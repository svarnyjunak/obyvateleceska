using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;
using System.Linq;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Queries
{
    public class QueryMunicipalityByCodeTest
    {
        [Fact]
        public void CodeProperty_Test()
        {
            var query = new QueryMunicipalityByCode { Code = "XXX" };
            Assert.Equal("XXX", query.Code);
        }

        [Fact]
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
            Assert.Equal("XXX", result?.MunicipalityId);
        }
    }
}
