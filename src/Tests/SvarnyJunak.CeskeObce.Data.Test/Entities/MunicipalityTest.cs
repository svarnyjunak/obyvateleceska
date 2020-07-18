using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Data.Test.Entities
{
    [TestClass]
    public class MunicipalityTest
    {
        [TestMethod]
        public void MunicipalityId_Test()
        {
            var municipality = new Municipality
            {
                MunicipalityId = "code"
            };

            Assert.AreEqual("code", municipality.MunicipalityId);
        }

        [TestMethod]
        public void DistrictName_Test()
        {
            var municipality = new Municipality
            {
                DistrictName = "district"
            };

            Assert.AreEqual("district", municipality.DistrictName);
        }

        [TestMethod]
        public void Latitude_Test()
        {
            var municipality = new Municipality
            {
                Latitude = 10.23m
            };

            Assert.AreEqual(10.23m, municipality.Latitude);
        }

        [TestMethod]
        public void Longitude_Test()
        {
            var municipality = new Municipality
            {
                Longitude = 20.23m
            };

            Assert.AreEqual(20.23m, municipality.Longitude);
        }
    }
}
