using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Web.Controllers
{
    public class SitemapController : Controller
    {
        private IMunicipalityRepository _municipalityRepository;

        public SitemapController(IMunicipalityRepository municipalityRepository)
        {
            _municipalityRepository = municipalityRepository;
        }

        [HttpGet]
        public void Index()
        {
            var municipalities = _municipalityRepository.GetMunicipalities();
            var host = new Uri(Request.Host.Value);
            var urls = municipalities.Select(m => CreateUrl(host, m));
        }

        private Uri CreateUrl(Uri host, Municipality municipality)
        {
            var relativePath = Url.Action("Index", "Home", new { district = municipality.DistrictName, name = municipality.Name, code = municipality.Code });
            return new Uri(host, relativePath);
        }
    }
}
