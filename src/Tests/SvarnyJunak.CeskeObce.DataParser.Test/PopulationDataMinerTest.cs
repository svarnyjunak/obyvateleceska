﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SvarnyJunak.CeskeObce.DataParser.Entities;

namespace SvarnyJunak.CeskeObce.DataParser.Test
{
    [TestClass]
    public class PopulationDataMinerTest
    {
        private readonly PopulationDataMiner _populationDataMiner = new PopulationDataMiner();

        [TestMethod]
        public void ComputePopulationProgressInMunicipalities_GroupTest()
        {
            _populationDataMiner.AddPopulationData(CreateDataWithTwoFramesAndOneMunicipality());

            var result = _populationDataMiner.ComputePopulationProgressInMunicipalities();
            var municipalityIds = result.GroupBy(p => p.MunicipalityId);
            Assert.AreEqual(1, municipalityIds.Count());
        }

        [TestMethod]
        public void ComputePopulationProgressInMunicipalities_ProgressValuesCountTest()
        {
            _populationDataMiner.AddPopulationData(CreateDataWithTwoFramesAndOneMunicipality());

            var result = _populationDataMiner.ComputePopulationProgressInMunicipalities();
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void ComputePopulationProgressInMunicipalities_DoNotGroupTest()
        {
            _populationDataMiner.AddPopulationData(CreateDataWithTwoFramesAndTwoMunicipalities());

            var result = _populationDataMiner.ComputePopulationProgressInMunicipalities();
            Assert.AreEqual(2, result.Count());
        }

        private IEnumerable<PopulationInMunicipalitity> CreateDataWithTwoFramesAndOneMunicipality()
        {
            yield return new PopulationInMunicipalitity
            {
                MunicipalityCode = "1",
                TotalCount = 10,
                Year = 2000,
            };

            yield return new PopulationInMunicipalitity
            {
                MunicipalityCode = "1",
                TotalCount = 20,
                Year = 2001,
            };
        }

        private IEnumerable<PopulationInMunicipalitity> CreateDataWithTwoFramesAndTwoMunicipalities()
        {
            yield return new PopulationInMunicipalitity
            {
                MunicipalityCode = "1",
                TotalCount = 1,
                Year = 2000,
            };

            yield return new PopulationInMunicipalitity
            {
                MunicipalityCode = "2",
                TotalCount = 1,
                Year = 2000,
            };
        }
    }
}
