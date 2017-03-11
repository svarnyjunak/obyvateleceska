using NSubstitute;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Web.Controllers;
using SvarnyJunak.CeskeObce.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SvarnyJunak.CeskeObce.Web.Test.Controllers
{
    public class HomeControllerTest
    {
        private IMunicipalityRepository _municipalityRepository;

        public HomeControllerTest()
        {
            _municipalityRepository = Substitute.For<IMunicipalityRepository>();
        }

        [Fact]
        public void IndexTest()
        {
            var municipality = CreateMunicipality();
            var populationProgress = new PopulationProgressInMunicipality();
            _municipalityRepository.GetMunicipalities().Returns(new[] { municipality });
            _municipalityRepository.GetPopulationProgress(municipality.Code).Returns(populationProgress);

            var controller = new HomeController(_municipalityRepository);
            var result = controller.Index();

            Assert.IsType<MunicipalityPopulationProgressModel>(result.Model);

            var model = (MunicipalityPopulationProgressModel)result.Model;
            Assert.Same(municipality, model.Municipality);
            Assert.Equal(populationProgress.PopulationProgress, model.PopulationProgress);
        }

        private Municipality CreateMunicipality()
        {
            return new Municipality
            {
                Code = "1",
                DistrictName = "Český Krumlov",
                Name = "Křemže",
            };
        }
    }
}
