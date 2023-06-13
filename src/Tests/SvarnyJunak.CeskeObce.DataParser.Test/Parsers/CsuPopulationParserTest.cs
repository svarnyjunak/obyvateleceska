using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.DataParser.Parsers;
using SvarnyJunak.CeskeObce.DataParser.Utils;

namespace SvarnyJunak.CeskeObce.DataParser.Test.Parsers
{
    [TestClass]
    public class CsuPopulationParserTest
    {
        [TestMethod]
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

            Assert.AreEqual(2000, frame.Year);
            Assert.AreEqual("MunicipalityId", frame.MunicipalityCode);

            Assert.AreEqual(20, frame.TotalCount);
            Assert.AreEqual(11, frame.MalesCount);
            Assert.AreEqual(9, frame.FemalesCount);

            Assert.AreEqual(40, frame.AverageAge);
            Assert.AreEqual(41, frame.MaleAverageAge);
            Assert.AreEqual(39, frame.FemaleAverageAge);
        }
    }
}
