using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;

namespace SvarnyJunak.CeskeObce.Data.Test.Repositories
{
    [TestClass]
    public class PopulationProgressRepositoryTest
    {
        //[TestMethod]
        //public void GetByMunicipality_Test()
        //{
        //    var dataLoader = Substitute.For<IDataLoader>();
        //    var progress = new PopulationProgressInMunicipality { MunicipalityCode = "XXX" };
        //    dataLoader.GetPopulationProgress().Returns(new[] { progress });

        //    var repository = new PopulationProgressRepository(dataLoader);

        //    var result = repository.GetByMunicipalityCode("XXX");
        //    Assert.AreEqual(progress, result);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void GetByMunicipality_NoResultTest()
        //{
        //    var dataLoader = Substitute.For<IDataLoader>();
        //    var repository = new PopulationProgressRepository(dataLoader);

        //    repository.GetByMunicipalityCode("XXX");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void GetByMunicipality_TooManyResultsTest()
        //{
        //    var dataLoader = Substitute.For<IDataLoader>();
        //    dataLoader.GetPopulationProgress().Returns(new[]
        //    {
        //        new PopulationProgressInMunicipality {MunicipalityCode = "XXX"},
        //        new PopulationProgressInMunicipality {MunicipalityCode = "XXX"}
        //    });

        //    var repository = new PopulationProgressRepository(dataLoader);

        //    repository.GetByMunicipalityCode("XXX");
        //}
    }
}
