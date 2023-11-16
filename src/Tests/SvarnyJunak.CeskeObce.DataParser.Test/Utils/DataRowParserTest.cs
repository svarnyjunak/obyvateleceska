using SvarnyJunak.CeskeObce.DataParser.Utils;
using System;
using Xunit;

namespace SvarnyJunak.CeskeObce.DataParser.Test.Utils
{
    public class DataRowParserTest
    {
        [Fact]
        public void ParseInt_Test()
        {
            var parser = new DataRowParser();

            Assert.Equal(1, parser.ParseInt("1"));
        }

        [Fact]
        public void ParseString_Test()
        {
            var parser = new DataRowParser();

            Assert.Equal("test", parser.ParseString("test"));
        }

        [Fact]
        public void ParseString_NullValueTest()
        {
            var parser = new DataRowParser();

            Assert.Equal(String.Empty, parser.ParseString(null));
        }
    }
}
