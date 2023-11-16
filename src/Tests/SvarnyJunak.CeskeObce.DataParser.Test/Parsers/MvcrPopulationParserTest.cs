using SvarnyJunak.CeskeObce.DataParser.Parsers;
using SvarnyJunak.CeskeObce.DataParser.Utils;
using System.Linq;
using Xunit;

namespace SvarnyJunak.CeskeObce.DataParser.Test.Parsers
{
    public class MvcrPopulationParserTest
    {
        [Fact]
        public void Parse_Test()
        {
            var emptyRow = new DataRow { Columns = new object[10] };

            var dataRow = new DataRow
            {
                Columns = new object[]
                {
                    "",
                    "Karlovarský",
                    "Ostrov",
                    "554979",
                    "Abertamy",
                    "432",
                    "389",
                    "420",
                    "364",
                    "852",
                    "753"
                }
            };

            var parser = new MvcrPopulationParser();
            var result = parser.Parse(new[] 
                { 
                    emptyRow,
                    emptyRow,
                    emptyRow,
                    emptyRow,
                    emptyRow,
                    emptyRow,
                    dataRow 
                }, 2000);

            var frame = result.Single();

            Assert.Equal(2000, frame.Year);
            Assert.Equal("554979", frame.MunicipalityCode);

            Assert.Equal(852, frame.TotalCount);
            Assert.Equal(432, frame.MalesCount);
            Assert.Equal(420, frame.FemalesCount);

            Assert.Equal(0, frame.AverageAge);
            Assert.Equal(0, frame.MaleAverageAge);
            Assert.Equal(0, frame.FemaleAverageAge);
        }
    }
}
