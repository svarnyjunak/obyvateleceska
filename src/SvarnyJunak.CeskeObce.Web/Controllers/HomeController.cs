using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson;
using SvarnyJunak.CeskeObce.Web.Models;
using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Web.Controllers
{
    public class HomeController : Controller
    {
        protected IMunicipalityRepository MunicipalityRepository { get; set; }
        protected MunicipalityCache MunicipalityCache { get; set; }

        public HomeController(IMunicipalityRepository municipalityRepository)
        {
            MunicipalityRepository = municipalityRepository;
            MunicipalityCache = new MunicipalityCache(municipalityRepository);
        }

        [HttpGet]
        public ViewResult Index(string district, string name, string code)
        {
            var municipality = code == null ? MunicipalityCache.GetRandomMunicipality() : MunicipalityCache.GetMunicipality(code);
            var populationProgress = MunicipalityRepository.GetPopulationProgress(municipality.Code);

            var model = new MunicipalityPopulationProgressModel
            {
                Municipality = municipality,
                PopulationProgress = populationProgress.PopulationProgress.OrderBy(f => f.Year).ToArray()
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult SelectMunicipality(string municipalityName)
        {
            var municipalities = FindMunicipalitiesByNameWithDiscrict(municipalityName);

            if (!municipalities.Any())
            {
                throw new Exception("Municipality not found");
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
            IEnumerable<Municipality> municipalities;
            if (municipalityName.Contains(","))
            {
                var parts = municipalityName.Split(',');

                municipalities = MunicipalityCache.FindByNameAndDistrict(parts[0], parts[1]);
            }
            else
            {
                municipalities = MunicipalityCache.FindByName(municipalityName);
            }

            return municipalities;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
