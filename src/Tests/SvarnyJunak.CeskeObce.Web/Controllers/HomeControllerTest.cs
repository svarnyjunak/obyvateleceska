using NSubstitute;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Web.Controllers;
using SvarnyJunak.CeskeObce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Index_Test()
        {
            var municipality = CreateMunicipality();
            var populationProgress = CreatePopulationProgress(municipality);
            _municipalityRepository.GetMunicipalities().Returns(new[] { municipality });
            _municipalityRepository.GetPopulationProgress(municipality.Code).Returns(populationProgress);

            var controller = new HomeController(_municipalityRepository);
            var result = controller.Index();

            Assert.IsType<MunicipalityPopulationProgressModel>(result.Model);

            var model = (MunicipalityPopulationProgressModel)result.Model;
            Assert.Same(municipality, model.Municipality);
            Assert.Equal(populationProgress.PopulationProgress, model.PopulationProgress);
        }

        [Fact]
        public void SelectMunicipality_Test()
        {
            var municipality = CreateMunicipality();
            var populationProgress = CreatePopulationProgress(municipality);
            _municipalityRepository.GetMunicipalities().Returns(new[] { municipality });
            _municipalityRepository.GetPopulationProgress(municipality.Code).Returns(populationProgress);

            var controller = new HomeController(_municipalityRepository);
            var result = controller.SelectMunicipality("křemže");

            var model = (MunicipalityPopulationProgressModel)result.Model;
            Assert.Same(municipality, model.Municipality);
            Assert.Equal(populationProgress.PopulationProgress, model.PopulationProgress);
        }

        [Fact]
        public void FindMunicipalities_PartialNameTest()
        {
            var municipality = CreateMunicipality();
            _municipalityRepository.GetMunicipalities().Returns(new[] { municipality });

            var controller = new HomeController(_municipalityRepository);
            var result = controller.FindMunicipalities("křem");
            Assert.IsType<String[]>(result.Value);

            var model = (String[])result.Value;
            Assert.Equal(1, model.Length);
            Assert.Equal("Křemže, Český Krumlov", model.Single());
        }

        [Fact]
        public void FindMunicipalities_FullNameWithDistrictTest()
        {
            var municipality = CreateMunicipality();
            _municipalityRepository.GetMunicipalities().Returns(new[] { municipality });

            var controller = new HomeController(_municipalityRepository);
            var result = controller.FindMunicipalities("křemže, český krumlov");
            Assert.IsType<String[]>(result.Value);

            var model = (String[])result.Value;
            Assert.Equal(1, model.Length);
            Assert.Equal("Křemže, Český Krumlov", model.Single());
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

        private PopulationProgressInMunicipality CreatePopulationProgress(Municipality municipality)
        {
            return new PopulationProgressInMunicipality
            {
                MunicipalityCode = municipality.Code
            };
        }
    }
}
