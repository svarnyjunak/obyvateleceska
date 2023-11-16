using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;
using System.Linq;
using Xunit;

namespace SvarnyJunak.CeskeObce.Data.Test.Queries
{
    public class QueryMunicipalityByNameAndDistrictTest
    {
        [Fact]
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
            Assert.Equal("Name", result?.Name);
            Assert.Equal("District", result?.DistrictName);
        }
    }
}
