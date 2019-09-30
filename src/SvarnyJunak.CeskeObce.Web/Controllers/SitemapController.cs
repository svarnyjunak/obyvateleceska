using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Net.Http.Headers;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvarnyJunak.CeskeObce.Web.Controllers
{
    [Route("sitemap.xml")]
    public class SitemapController : Controller
    {
        private readonly IDataLoader _dataLoader;

        public SitemapController(IDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        [HttpGet]
        public FileContentResult Index()
        {
            var municipalities = _dataLoader.GetMunicipalities();
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
            var routeValues = new { district = municipality.DistrictName, name = municipality.Name, code = municipality.Code };
            string scheme = urlHelper.ActionContext.HttpContext.Request.Scheme;
            return urlHelper.RouteUrl("MunicipalityRoute", routeValues, scheme);
        }
    }
}
