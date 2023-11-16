using System.Linq;
using SvarnyJunak.CeskeObce.DataParser.Parsers;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using Xunit;

namespace SvarnyJunak.CeskeObce.DataParser.Test.Parsers
{
    public class CsuPopulationParserTest
    {
        [Fact]
        public void Parse_Test()
        {

            var headerRow = new DataRow
            {
                Columns = new object[5]
            };

            var dataRow = new DataRow
            {
                Columns = new object[]
                {
                    "CZ 1",
                    "MunicipalityId",
                    string.Empty,
                    "20",
                    "11",
                    "9",
                    "40",
                    "41",
                    "39",
                }
            };

            var parser = new CsuPopulationParser();
            var result = parser.Parse(new[] { headerRow, dataRow }, 2000);

            var frame = result.Single();

            Assert.Equal(2000, frame.Year);
            Assert.Equal("MunicipalityId", frame.MunicipalityCode);

            Assert.Equal(20, frame.TotalCount);
            Assert.Equal(11, frame.MalesCount);
            Assert.Equal(9, frame.FemalesCount);

            Assert.Equal(40, frame.AverageAge);
            Assert.Equal(41, frame.MaleAverageAge);
            Assert.Equal(39, frame.FemaleAverageAge);
        }
    }
}
