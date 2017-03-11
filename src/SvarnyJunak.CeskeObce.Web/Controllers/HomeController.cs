using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson;
using SvarnyJunak.CeskeObce.Web.Models;

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
        public ViewResult Index()
        {
            var municipality = MunicipalityCache.GetRandomMunicipality();
            var populationProgress = MunicipalityRepository.GetPopulationProgress(municipality.Code);

            var model = new MunicipalityPopulationProgressModel
            {
                Municipality = municipality,
                PopulationProgress = populationProgress.PopulationProgress.OrderBy(f => f.Year).ToArray()
            };

            return View(model);
        }

        public IActionResult FindMunicipalities(string name)
        {
            var municipalities = MunicipalityCache.FindByName(name);
            var data = municipalities.Select(m => m.Name + ", " + m.DistrictName);
            return Json(data);
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
