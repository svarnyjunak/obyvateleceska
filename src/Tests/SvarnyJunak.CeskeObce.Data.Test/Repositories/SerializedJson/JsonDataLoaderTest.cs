using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories.SerializedJson
{
    [TestClass]
    public class JsonDataLoaderTest
    {
        private readonly JsonDataLoader _jsonDataLoader = new JsonDataLoader();

        [TestMethod]
        public void Load_Test()
        {
            Assert.IsTrue(_jsonDataLoader.GetPopulationProgress().Any());
            Assert.IsTrue(_jsonDataLoader.GetMunicipalities().Any());
        }
    }
}
