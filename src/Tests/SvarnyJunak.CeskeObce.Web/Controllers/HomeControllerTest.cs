using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Web.Controllers;
using SvarnyJunak.CeskeObce.Web.Models;
using System.Collections.Generic;
using System.Linq;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;
using Xunit;

namespace SvarnyJunak.CeskeObce.Web.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index_SpecificMunicipalityTest()
        {
            var municipality = new Municipality
            {
                MunicipalityId = "19",
                DistrictName = "Český Krumlov",
                Name = "Křemže"
            };
            var populationProgress = CreatePopulationProgress(municipality).ToArray();

            var municipalityRepository = Substitute.For<IMunicipalityRepository>();
            var populationFrameRepository = Substitute.For<IPopulationFrameRepository>();

            municipalityRepository.GetByCode("19").Returns(municipality);
            populationFrameRepository
                .FindAll(Arg.Is<QueryPopulationFrameByMunicipalityCode>(q => q.Code == "19"))
                .Returns(populationProgress);

            var controller = new HomeController(municipalityRepository, populationFrameRepository);
            var result = controller.Index(null, null, "19");

            Assert.IsType<MunicipalityPopulationProgressModel>(result.Model);

            var model = (MunicipalityPopulationProgressModel)result.Model;
            Assert.Equal("19", model.Municipality.MunicipalityId);
        }

        [Fact]
        public void Index_RandomMunicipalityTest()
        {
            var municipality = CreateMunicipality();
            var populationProgress = CreatePopulationProgress(municipality).ToArray();

            var municipalityRepository = Substitute.For<IMunicipalityRepository>();
            var populationFrameRepository = Substitute.For<IPopulationFrameRepository>();

            municipalityRepository.GetRandom().Returns(municipality);
            populationFrameRepository
                .FindAll(Arg.Is<QueryPopulationFrameByMunicipalityCode>(q => q.Code == municipality.MunicipalityId))
                .ReturnsForAnyArgs(populationProgress);

            var controller = new HomeController(municipalityRepository, populationFrameRepository);
            var result = controller.Index(null, null, null);

            Assert.IsType<MunicipalityPopulationProgressModel>(result.Model);

            var model = (MunicipalityPopulationProgressModel)result.Model;
            Assert.Same(municipality, model.Municipality);

            var expectedPopulation = populationProgress.OrderByDescending(d => d.Year);
            Assert.Equal(expectedPopulation, model.PopulationProgress);
        }

        [Fact]
        public void SelectMunicipality_Test()
        {
            var municipality = CreateMunicipality();

            var municipalityRepository = Substitute.For<IMunicipalityRepository>();
            var populationFrameRepository = Substitute.For<IPopulationFrameRepository>();

            municipalityRepository
                .FindAll(Arg.Is<QueryMunicipalityByName>(q => q.Name == "křemže"))
                .Returns(new[] {municipality});

            var controller = new HomeController(municipalityRepository, populationFrameRepository);
            var result = controller.SelectMunicipality("křemže", null);

            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal(municipality.MunicipalityId, redirectResult.RouteValues["code"]);
            Assert.Equal("cesky-krumlov", redirectResult.RouteValues["district"]);
            Assert.Equal("kremze", redirectResult.RouteValues["name"]);
        }

        [Fact]
        public void SelectMunicipality_SimilarNamesTest()
        {
            var municipalityKurim = CreateMunicipality();
            municipalityKurim.Name = "Kuřim";

            var municipalityKurimskeJestrabi = CreateMunicipality();
            municipalityKurimskeJestrabi.Name = "Kuřimské Jestřábí";

            var municipalityRepository = Substitute.For<IMunicipalityRepository>();
            var populationFrameRepository = Substitute.For<IPopulationFrameRepository>();

            municipalityRepository
               .FindAll(Arg.Is<QueryMunicipalityByName>(q => q.Name == "kuřim"))
               .Returns(new[] { municipalityKurimskeJestrabi, municipalityKurim });

            var controller = new HomeController(municipalityRepository, populationFrameRepository);
            var result = controller.SelectMunicipality("kuřim", null);

            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = (RedirectToActionResult)result;

            Assert.Equal("kurim", redirectResult.RouteValues["name"]);
        }

        [Fact]
        public void SelectMunicipality_NotFoundTest()
        {
            var municipality = CreateMunicipality();

            var municipalityRepository = Substitute.For<IMunicipalityRepository>();
            var populationFrameRepository = Substitute.For<IPopulationFrameRepository>();

            municipalityRepository.GetByCode("1").Returns(municipality);

            var controller = new HomeController(municipalityRepository, populationFrameRepository);
            var result = controller.SelectMunicipality("not existing municipality", "1");

            Assert.IsType<ViewResult>(result);

            var viewResult = (ViewResult)result;
            var model = (MunicipalityPopulationProgressModel)viewResult.Model;
            Assert.Same(municipality, model.Municipality);
            Assert.Equal("not existing municipality", model.MunicipalityNameSearch);

            var modelState = controller.ViewData.ModelState;
            Assert.True(modelState.ContainsKey("MunicipalityNameSearch"), "Error in model state is missing.");
            Assert.True(modelState.ContainsKey("MunicipalityNameSearch"), "Error in model state is missing.");
        }

        [Fact]
        public void SelectMunicipality_NullTest()
        {
            var municipality = CreateMunicipality();

            var municipalityRepository = Substitute.For<IMunicipalityRepository>();
            var populationFrameRepository = Substitute.For<IPopulationFrameRepository>();

            municipalityRepository
                .GetByCode("1")
                .Returns(municipality);

            var controller = new HomeController(municipalityRepository, populationFrameRepository);
            var result = controller.SelectMunicipality(null!, "1");

            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var model = (MunicipalityPopulationProgressModel)viewResult.Model;
            Assert.Same(municipality, model.Municipality);
        }

        [Fact]
        public void FindMunicipalities_PartialNameTest()
        {
            var municipality = CreateMunicipality();

            var municipalityRepository = Substitute.For<IMunicipalityRepository>();
            var populationFrameRepository = Substitute.For<IPopulationFrameRepository>();

            municipalityRepository
                .FindAll(Arg.Is<QueryMunicipalityByName>(q => q.Name == "křem"))
                .Returns(new[] {municipality});

            var controller = new HomeController(municipalityRepository, populationFrameRepository);
            var model = GetOkResultValue(controller.FindMunicipalities("křem"));
            Assert.Single(model);
            Assert.Equal("Křemže, Český Krumlov", model.Single());
        }

        [Fact]
        public void FindMunicipalities_FullNameWithDistrictTest()
        {
            var municipality = CreateMunicipality();

            var municipalityRepository = Substitute.For<IMunicipalityRepository>();
            var populationFrameRepository = Substitute.For<IPopulationFrameRepository>();

            municipalityRepository
                .FindAll(Arg.Any<QueryMunicipalityByNameAndDistrict>())
                .Returns(new[] {municipality});

            var controller = new HomeController(municipalityRepository, populationFrameRepository);
            var model = GetOkResultValue(controller.FindMunicipalities("křemže, český krumlov"));
            Assert.Single(model);
            Assert.Equal("Křemže, Český Krumlov", model.Single());
        }

        private Municipality CreateMunicipality()
        {
            return new Municipality
            {
                MunicipalityId = "1",
                DistrictName = "Český Krumlov",
                Name = "Křemže",
            };
        }

        private IEnumerable<Municipality> CreateMunicipalities(int count)
        {
            for(int i = 0; i < count;i++)
            {
                var municipality = CreateMunicipality();
                municipality.MunicipalityId = i.ToString();
                yield return municipality;
            }
        }

        private IEnumerable<PopulationFrame> CreatePopulationProgress(Municipality municipality)
        {
            yield return new PopulationFrame {Count = 1, Year = 2000};
            yield return new PopulationFrame {Count = 2, Year = 2001};
        }

        private static T GetOkResultValue<T>(ActionResult<T> actionResult)
        {
            Assert.IsType<OkObjectResult>(actionResult.Result);

            var okResult = (OkObjectResult)actionResult.Result;
            Assert.IsType<T>(okResult.Value);
            return (T) okResult.Value;
        }
    }
}
