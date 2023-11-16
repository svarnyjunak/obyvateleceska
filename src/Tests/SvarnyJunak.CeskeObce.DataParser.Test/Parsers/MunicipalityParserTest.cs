using SvarnyJunak.CeskeObce.DataParser.Parsers;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using System.Linq;
using Xunit;

namespace SvarnyJunak.CeskeObce.DataParser.Test.Parsers
{
    public class MunicipalityParserTest
    {
        [Fact]
        public void Parse_Test()
        {
            var headerRow = new DataRow();

            var dataRow = new DataRow
            {
                Columns = new object[]
                {
                    "MunicipalityId",
                    "Name",
                    "DistrictName",
                    11.1m,
                    22.2m,
                }
            };

            var parser = new MunicipalityParser();
            var result = parser.Parse(new[] { headerRow, dataRow });

            var municipality = result.Single();

            Assert.Equal("MunicipalityId", municipality.MunicipalityId);
            Assert.Equal("Name", municipality.Name);
            Assert.Equal("DistrictName", municipality.DistrictName);
            Assert.Equal(11.1m, municipality.Latitude);
            Assert.Equal(22.2m, municipality.Longitude);
        }
    }
}
