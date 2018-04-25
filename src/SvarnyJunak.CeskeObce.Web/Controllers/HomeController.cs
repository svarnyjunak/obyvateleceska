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
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;

namespace SvarnyJunak.CeskeObce.Web.Controllers
{
    public class HomeController : Controller
    {
        protected MunicipalityRepository MunicipalityRepository { get; set; }
        protected PopulationProgressRepository PopulationProgressRepository { get; set; }
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IDataLoader dataLoader, IStringLocalizer<HomeController> localizer)
        {
            MunicipalityRepository = new MunicipalityRepository(dataLoader);
            PopulationProgressRepository = new PopulationProgressRepository(dataLoader);

            _localizer = localizer;
        }

        [HttpGet]
        public ViewResult Index(string district, string name, string code)
        {
            var model = code == null ? CreateRandomModel() : CreateModelByCode(code);
            ViewData["MetaDescription"] = CreateMetaDescription(model);
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
                return View(nameof(Index), model);
            }

            var municipality = municipalities.First();
            return RedirectToAction(nameof(Index), new { district = municipality.DistrictName, name = municipality.Name, code = municipality.Code });
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

            IQuery<Municipality> query;
            if (municipalityName.Contains(","))
            {
                var parts = municipalityName.Split(',');
                query = new QueryMunicipalityByNameAndDistrict
                {
                    Name = parts[0],
                    District = parts[1]
                };
            }
            else
            {
                query = new QueryMunicipalityByName
                {
                    Name = municipalityName
                };
            }

            return MunicipalityRepository.FindAll(query);
        }

        private MunicipalityPopulationProgressModel CreateModelByCode(string code)
        {
            var municipality = MunicipalityRepository.GetByCode(code);
            var populationProgress = PopulationProgressRepository.GetByMunicipalityCode(code);

            return new MunicipalityPopulationProgressModel
            {
                Municipality = municipality,
                PopulationProgress = populationProgress.PopulationProgress.ToArray()
            };
        }

        private MunicipalityPopulationProgressModel CreateRandomModel()
        {
            var municipality = MunicipalityRepository.GetRandom();
            var populationProgress = PopulationProgressRepository.GetByMunicipalityCode(municipality.Code);

            return new MunicipalityPopulationProgressModel
            {
                Municipality = municipality,
                PopulationProgress = populationProgress.PopulationProgress.ToArray()
            };
        }

        private string CreateMetaDescription(MunicipalityPopulationProgressModel model)
        {
            var years = model.PopulationProgress.Select(p => p.Year).ToArray();
            var firstYear = years.Min();
            var lastYear = years.Max();
            return $"Vývoj počtu obyvatel v obci {model.Municipality.Name} (okres {model.Municipality.DistrictName}) mezi roky {firstYear} - {lastYear}.";
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
