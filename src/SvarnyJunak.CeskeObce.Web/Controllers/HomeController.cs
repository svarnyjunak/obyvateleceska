using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson;
using SvarnyJunak.CeskeObce.Web.Models;
using SvarnyJunak.CeskeObce.Data.Entities;
using Microsoft.Extensions.Localization;

namespace SvarnyJunak.CeskeObce.Web.Controllers
{
    public class HomeController : Controller
    {
        protected IMunicipalityRepository MunicipalityRepository { get; set; }
        protected MunicipalityCache MunicipalityCache { get; set; }
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IMunicipalityRepository municipalityRepository, IStringLocalizer<HomeController> localizer)
        {
            MunicipalityRepository = municipalityRepository;
            MunicipalityCache = new MunicipalityCache(municipalityRepository);
            _localizer = localizer;
        }

        [HttpGet]
        public ViewResult Index(string district, string name, string code)
        {
            var model = code == null ? CreateRandomModel() : CreateModelByCode(code);
            return View(model);
        }

        [HttpPost]
        public IActionResult SelectMunicipality(string municipalityName, string currentMunicipalityCode)
        {
            var municipalities = FindMunicipalitiesByNameWithDiscrict(municipalityName);

            if (!municipalities.Any())
            {
                var model = CreateModelByCode(currentMunicipalityCode);
                model.MunicipalityNameSearch = municipalityName;

                var errorMessage = Resources.Controllers_HomeController.No_municipality_found_;
                ModelState.AddModelError(nameof(model.MunicipalityNameSearch), errorMessage);
                return View("Index", model);
            }

            var municipality = municipalities.First();
            return RedirectToAction("Index", new { district = municipality.DistrictName, name = municipality.Name, code = municipality.Code });
        }

        [HttpPost]
        public JsonResult FindMunicipalities(string name)
        {
            var municipalities = FindMunicipalitiesByNameWithDiscrict(name);
            var data = municipalities.Select(m => $"{m.Name}, {m.DistrictName}").ToArray();
            return Json(data);
        }

        private IEnumerable<Municipality> FindMunicipalitiesByNameWithDiscrict(string municipalityName)
        {
            if (municipalityName == null)
            {
                return new Municipality[0];
            }

            if (municipalityName.Contains(","))
            {
                var parts = municipalityName.Split(',');

                return MunicipalityCache.FindByNameAndDistrict(parts[0], parts[1]);
            }

            return MunicipalityCache.FindByName(municipalityName);
        }

        private MunicipalityPopulationProgressModel CreateModelByCode(string code)
        {
            var municipality = MunicipalityCache.GetMunicipality(code);
            var populationProgress = MunicipalityRepository.GetPopulationProgress(code);

            return new MunicipalityPopulationProgressModel
            {
                Municipality = municipality,
                PopulationProgress = populationProgress.PopulationProgress.ToArray()
            };
        }

        private MunicipalityPopulationProgressModel CreateRandomModel()
        {
            var municipality = MunicipalityCache.GetRandomMunicipality();
            var populationProgress = MunicipalityRepository.GetPopulationProgress(municipality.Code);

            return new MunicipalityPopulationProgressModel
            {
                Municipality = municipality,
                PopulationProgress = populationProgress.PopulationProgress.ToArray()
            };
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
