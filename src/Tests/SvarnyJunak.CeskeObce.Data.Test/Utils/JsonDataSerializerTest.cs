using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Data.Test.Utils
{
    [TestClass]
    public class JsonDataSerializerTest
    {
        private readonly JsonDataSerializer _jsonDataSerializer = new JsonDataSerializer();

        [TestMethod]
        public void GetDataExtension_Test()
        {
            Assert.AreEqual("js", _jsonDataSerializer.GetDataExtension());
        }

        [TestMethod]
        public void Write_Test()
        {
            var data = new PopulationFrame
            {
                Count = 1,
                Year = 2000
            };

            Assert.AreEqual("{\"Year\":2000,\"Count\":1}", _jsonDataSerializer.Write(data));
        }

        [TestMethod]
        public void Read_Test()
        {
            var json = "{\"Year\":2000,\"Count\":1}";
            var data = _jsonDataSerializer.Read<PopulationFrame>(json);
            Assert.AreEqual(2000, data.Year);
            Assert.AreEqual(1, data.Count);
        }
    }
}
