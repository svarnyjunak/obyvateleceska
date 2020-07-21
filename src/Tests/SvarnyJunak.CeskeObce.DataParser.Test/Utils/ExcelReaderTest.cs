using SvarnyJunak.CeskeObce.DataParser.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SvarnyJunak.CeskeObce.DataParser.Test.Utils
{
    [TestClass]
    public class ExcelReaderTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadData_ThrowsExceptionWhenFileDoesntExist()
        {
            var reader = new ExcelReader();
            var fileName = GetFilePath("NotExistingFile.xlsx");
            var x = reader.ReadData(fileName, "List1").ToArray();
        }

        [TestMethod]
        public void ReadData_BasicTest()
        {
            var reader = new ExcelReader();
            var file = GetFilePath("BasicTest.xlsx");
            var result = reader.ReadData(file, "List1");

            var singleValue = result.Single().Columns.Single();
            Assert.AreEqual("Test", singleValue);
        }

        private string GetFilePath(string file)
        {
            var currentAssemblyPath = typeof(ExcelReaderTest).GetTypeInfo().Assembly.Location;
            var currentPath = Path.GetDirectoryName(currentAssemblyPath);
            return Path.Combine(currentPath, "Utils", file);
        }
    }
}
