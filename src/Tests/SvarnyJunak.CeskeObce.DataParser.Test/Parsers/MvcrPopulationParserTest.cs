using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.DataParser.Parsers;
using SvarnyJunak.CeskeObce.DataParser.Utils;

namespace SvarnyJunak.CeskeObce.DataParser.Test.Parsers
{
    [TestClass]
    public class MvcrPopulationParserTest
    {
        [TestMethod]
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

            Assert.AreEqual(2000, frame.Year);
            Assert.AreEqual("554979", frame.MunicipalityCode);

            Assert.AreEqual(852, frame.TotalCount);
            Assert.AreEqual(432, frame.MalesCount);
            Assert.AreEqual(420, frame.FemalesCount);

            Assert.AreEqual(0, frame.AverageAge);
            Assert.AreEqual(0, frame.MaleAverageAge);
            Assert.AreEqual(0, frame.FemaleAverageAge);
        }
    }
}
