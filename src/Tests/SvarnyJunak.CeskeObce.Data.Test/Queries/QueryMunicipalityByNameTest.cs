using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;
using System.Linq;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Queries
{
    public class QueryMunicipalityByNameTest
    {
        [Fact]
        public void Expression_Test()
        {
            var query = new QueryMunicipalityByName
            {
                Name = "Name"
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
                },
                new Municipality
                {
                    Name = "B"
                },
            };

            var result = data.SingleOrDefault(query.Expression.Compile());
            Assert.Equal("Name", result?.Name);
        }
    }
}
