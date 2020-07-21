using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.DataParser.Utils;

namespace SvarnyJunak.CeskeObce.DataParser.Test
{
    [TestClass]
    public class MunicipalityParserTest
    {
        [TestMethod]
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
            var result = parser.Parse(new []{ headerRow, dataRow });

            var municipality = result.Single();

            Assert.AreEqual("MunicipalityId", municipality.MunicipalityId);
            Assert.AreEqual("Name", municipality.Name);
            Assert.AreEqual("DistrictName", municipality.DistrictName);
            Assert.AreEqual(11.1m, municipality.Latitude);
            Assert.AreEqual(22.2m, municipality.Longitude);
        }
    }
}
