﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NSubstitute;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Web.Controllers;
using SvarnyJunak.CeskeObce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace SvarnyJunak.CeskeObce.Web.Test.Controllers
{
    public class HomeControllerTest
    {
        private readonly IDataLoader _dataLoader;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeControllerTest()
        {
            _dataLoader = Substitute.For<IDataLoader>();
            _localizer = Substitute.For<IStringLocalizer<HomeController>>();
            _localizer.GetString(String.Empty).ReturnsForAnyArgs(c => new LocalizedString(c.Arg<string>(), c.Arg<string>()));
        }

        [Fact]
        public void Index_SpecificMunicipalityTest()
        {
            var municipalities = CreateMunicipalities(20).ToArray();
            _dataLoader.GetMunicipalities().Returns(municipalities);
            _dataLoader.GetPopulationProgress().Returns(CreatePopulationProgress(municipalities));

            var controller = new HomeController(_dataLoader, _localizer);
            var result = controller.Index(null, null, "19");

            Assert.IsType<MunicipalityPopulationProgressModel>(result.Model);

            var model = (MunicipalityPopulationProgressModel)result.Model;
            Assert.Equal("19", model.Municipality.Code);
        }

        [Fact]
        public void Index_RandomMunicipalityTest()
        {
            var municipality = CreateMunicipality();
            var populationProgress = CreatePopulationProgress(municipality);

            _dataLoader.GetMunicipalities().Returns(new[] { municipality });
            _dataLoader.GetPopulationProgress().Returns(new[] { populationProgress });

            var controller = new HomeController(_dataLoader, _localizer);
            var result = controller.Index(null, null, null);

            Assert.IsType<MunicipalityPopulationProgressModel>(result.Model);

            var model = (MunicipalityPopulationProgressModel)result.Model;
            Assert.Same(municipality, model.Municipality);

            var expectedPopulation = populationProgress.PopulationProgress.OrderByDescending(d => d.Year);
            Assert.Equal(expectedPopulation, model.PopulationProgress);
        }

        [Fact]
        public void SelectMunicipality_Test()
        {
            var municipality = CreateMunicipality();
            var populationProgress = CreatePopulationProgress(municipality);

            _dataLoader.GetMunicipalities().Returns(new[] { municipality });
            _dataLoader.GetPopulationProgress().Returns(new[] { populationProgress });

            var controller = new HomeController(_dataLoader, _localizer);
            var result = controller.SelectMunicipality("křemže", null);

            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal(municipality.Code, redirectResult.RouteValues["code"]);
            Assert.Equal(municipality.DistrictName, redirectResult.RouteValues["district"]);
            Assert.Equal(municipality.Name, redirectResult.RouteValues["name"]);
        }

        [Fact]
        public void SelectMunicipality_NotFoundTest()
        {
            var municipality = CreateMunicipality();
            var populationProgress = CreatePopulationProgress(municipality);

            _dataLoader.GetMunicipalities().Returns(new[] { municipality });
            _dataLoader.GetPopulationProgress().Returns(new[] { populationProgress });

            var controller = new HomeController(_dataLoader, _localizer);
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
            var populationProgress = CreatePopulationProgress(municipality);

            _dataLoader.GetMunicipalities().Returns(new[] { municipality });
            _dataLoader.GetPopulationProgress().Returns(new[] { populationProgress });

            var controller = new HomeController(_dataLoader, _localizer);
            var result = controller.SelectMunicipality(null, "1");

            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var model = (MunicipalityPopulationProgressModel)viewResult.Model;
            Assert.Same(municipality, model.Municipality);
        }

        [Fact]
        public void FindMunicipalities_PartialNameTest()
        {
            var municipality = CreateMunicipality();
            _dataLoader.GetMunicipalities().Returns(new[] { municipality });

            var controller = new HomeController(_dataLoader, _localizer);
            var model = GetOkResultValue(controller.FindMunicipalities("křem"));
            Assert.Single(model);
            Assert.Equal("Křemže, Český Krumlov", model.Single());
        }

        [Fact]
        public void FindMunicipalities_FullNameWithDistrictTest()
        {
            var municipality = CreateMunicipality();
            _dataLoader.GetMunicipalities().Returns(new[] { municipality });

            var controller = new HomeController(_dataLoader, _localizer);
            var model = GetOkResultValue(controller.FindMunicipalities("křemže, český krumlov"));
            Assert.Single(model);
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

        private IEnumerable<Municipality> CreateMunicipalities(int count)
        {
            for(int i = 0; i < count;i++)
            {
                var municipality = CreateMunicipality();
                municipality.Code = i.ToString();
                yield return municipality;
            }
        }

        private PopulationProgressInMunicipality CreatePopulationProgress(Municipality municipality)
        {
            return new PopulationProgressInMunicipality
            {
                MunicipalityCode = municipality.Code,
                PopulationProgress = new[]
                {
                    new PopulationFrame {Count = 1, Year = 2000},
                    new PopulationFrame {Count = 2, Year = 2001},
                }
            };
        }

        private IEnumerable<PopulationProgressInMunicipality> CreatePopulationProgress(IEnumerable<Municipality> municipalities)
        {
            return municipalities.Select(CreatePopulationProgress);
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
