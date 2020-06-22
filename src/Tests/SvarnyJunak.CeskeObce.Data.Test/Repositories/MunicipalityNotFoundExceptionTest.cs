using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.Data.Repositories;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories
{
    [TestClass]
    public class MunicipalityNotFoundExceptionTest
    {
        [TestMethod]
        public void Constructor_Test()
        {
            var ex = new MunicipalityNotFoundException("test");
            Assert.AreEqual("test", ex.MunicipalityName);
        }
    }
}
