using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.DataParser.Utils;

namespace SvarnyJunak.CeskeObce.DataParser.Test.Utils
{
    [TestClass]
    public class DataRowParserTest
    {
        [TestMethod]
        public void ParseInt_Test()
        {
            var parser = new DataRowParser();

            Assert.AreEqual(1, parser.ParseInt("1"));
        }

        [TestMethod]
        public void ParseString_Test()
        {
            var parser = new DataRowParser();

            Assert.AreEqual("test", parser.ParseString("test"));
        }

        [TestMethod]
        public void ParseString_NullValueTest()
        {
            var parser = new DataRowParser();

            Assert.AreEqual(String.Empty, parser.ParseString(null));
        }
    }
}
