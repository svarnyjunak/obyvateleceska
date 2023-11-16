using SvarnyJunak.CeskeObce.DataParser.Utils;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace SvarnyJunak.CeskeObce.DataParser.Test.Utils
{
    public class ExcelReaderTest
    {
        [Fact]
        public void ReadData_ThrowsExceptionWhenFileDoesNotExist()
        {
            var reader = new ExcelReader();
            var fileName = GetFilePath("NotExistingFile.xlsx");
            Assert.Throws<ArgumentException>(() => reader.ReadData(fileName, "List1").ToArray());
        }

        [Fact]
        public void ReadData_BasicTest()
        {
            var reader = new ExcelReader();
            var file = GetFilePath("BasicTest.xlsx");
            var result = reader.ReadData(file, "List1");

            var singleValue = result.Single().Columns.Single();
            Assert.Equal("Test", singleValue);
        }

        private string GetFilePath(string file)
        {
            var currentAssemblyPath = typeof(ExcelReaderTest).GetTypeInfo().Assembly.Location;
            var currentPath = Path.GetDirectoryName(currentAssemblyPath);
            return Path.Combine(currentPath, "Utils", file);
        }
    }
}
