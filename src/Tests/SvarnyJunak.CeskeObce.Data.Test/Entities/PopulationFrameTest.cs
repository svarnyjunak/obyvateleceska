using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Data.Test.Entities
{
    [TestClass]
    public class PopulationFrameTest
    {
        [TestMethod]
        public void Year_Test()
        {
            var populationFrame = new PopulationFrame
            {
                Year = 2000
            };

            Assert.AreEqual(2000, populationFrame.Year);
        }

        [TestMethod]
        public void Count_Test()
        {
            var populationFrame = new PopulationFrame
            {
                Count = 11
            };
            
            Assert.AreEqual(11, populationFrame.Count);
        }
    }
}
