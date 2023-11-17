using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Web.Utils;
using System.Text;
using System.Xml.Linq;

namespace SvarnyJunak.CeskeObce.Web.Controllers;

[Route("sitemap.xml")]
public class SitemapController : Controller
{
    private readonly IMunicipalityRepository _municipalityRepository;

    public SitemapController(IMunicipalityRepository municipalityRepository)
    {
        _municipalityRepository = municipalityRepository;
    }

    [HttpGet]
    public FileContentResult Index()
    {
        var urls = _municipalityRepository
            .FindAll()
            .Select(UrlUtils.CreateCanonicalUrl)
            .ToArray();

        XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var root = new XElement(xmlns + "urlset");
        var nodes = urls.Select(url => new XElement(xmlns + "url", new XElement(xmlns + "loc", url)));
        root.Add(nodes);
        var xml = new XDocument(root);
        var bytes = Encoding.ASCII.GetBytes(xml.ToString());

        return new FileContentResult(bytes, MediaTypeHeaderValue.Parse("application/xml"));
    }
}
