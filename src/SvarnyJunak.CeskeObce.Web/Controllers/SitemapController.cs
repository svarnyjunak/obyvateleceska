using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SvarnyJunak.CeskeObce.Data.Entities;
using System.Text;
using System.Xml.Linq;
using SvarnyJunak.CeskeObce.Data.Repositories;

namespace SvarnyJunak.CeskeObce.Web.Controllers
{
    [Route("sitemap.xml")]
    public class SitemapController : Controller
    {
        private readonly MunicipalityRepository _municipalityRepository;

        public SitemapController(MunicipalityRepository municipalityRepository)
        {
            _municipalityRepository = municipalityRepository;
        }

        [HttpGet]
        public FileContentResult Index()
        {
            var municipalities = _municipalityRepository.FindAll();
            var urls = municipalities.Select(m => CreateUrl(Url, m));

            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var root = new XElement(xmlns + "urlset");
            var nodes = urls.Select(url => new XElement(xmlns + "url", new XElement(xmlns + "loc", url)));
            root.Add(nodes);
            var xml = new XDocument(root);
            var bytes = Encoding.ASCII.GetBytes(xml.ToString());

            return new FileContentResult(bytes, MediaTypeHeaderValue.Parse("application/xml"));
        }

        private static string CreateUrl(IUrlHelper urlHelper, Municipality municipality)
        {
            var routeValues = new { district = municipality.DistrictName, name = municipality.Name, code = municipality.MunicipalityId };
            string scheme = urlHelper.ActionContext.HttpContext.Request.Scheme;
            return urlHelper.RouteUrl("MunicipalityRoute", routeValues, scheme);
        }
    }
}
