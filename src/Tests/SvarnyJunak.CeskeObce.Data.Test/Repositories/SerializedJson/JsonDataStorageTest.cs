using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories.SerializedJson
{
    [TestClass]
    public class JsonDataStorageTest
    {
        [TestMethod]
        public void Constructor_Test()
        {
            var test = new JsonDataStorage(new JsonDataSerializer(), "data");
            Assert.IsNotNull(test);//only for code coverage
        }

        [TestMethod]
        public void StoreMunicipalities_Test()
        {
            var fileStorage = Substitute.For<IFileStorage>();
            
            var jsonDataStorage = new JsonDataStorage(new JsonDataSerializer(), "data", fileStorage);
            jsonDataStorage.StoreMunicipalities(new[] {new Municipality {Code = "code", Name = "name"}});

            fileStorage
                .Received(Quantity.Exactly(1))
                .Store("data\\municipalities.js", "[{\"Code\":\"code\",\"Name\":\"name\",\"DistrictName\":null,\"Latitude\":0,\"Longitude\":0}]");
        }

        [TestMethod]
        public void StorePopulationProgress_Test()
        {
            var fileStorage = Substitute.For<IFileStorage>();

            var jsonDataStorage = new JsonDataStorage(new JsonDataSerializer(), "data", fileStorage);

            jsonDataStorage.StorePopulationProgress(new[]
                {new PopulationProgressInMunicipality {MunicipalityCode = "code"}});

            fileStorage
                .Received(Quantity.Exactly(1))
                .Store("data\\progress.js", "[{\"MunicipalityCode\":\"code\",\"PopulationProgress\":[]}]");
        }
    }
}
