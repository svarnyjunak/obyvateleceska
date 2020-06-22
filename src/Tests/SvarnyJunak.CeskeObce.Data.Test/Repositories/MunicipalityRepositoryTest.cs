using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories
{
    [TestClass]
    public class MunicipalityRepositoryTest
    {
        [TestMethod]
        public void Exists_Test()
        {
            var dataLoader = Substitute.For<IDataLoader>();
            var repository = new MunicipalityRepository(dataLoader);
            var result = repository.Exists(new QueryMunicipalityByCode());
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(MunicipalityNotFoundException))]
        public void GetByCode_NoCodeTest()
        {
            var dataLoader = Substitute.For<IDataLoader>();
            var repository = new MunicipalityRepository(dataLoader);

            repository.GetByCode("XXX");
        }
    }
}
