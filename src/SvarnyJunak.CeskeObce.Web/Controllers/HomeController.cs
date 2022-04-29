using Microsoft.AspNetCore.Mvc;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.Web.Models;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories.Queries;
using SvarnyJunak.CeskeObce.Data.Utils;
using SvarnyJunak.CeskeObce.Web.Utils;

namespace SvarnyJunak.CeskeObce.Web.Controllers;

public class HomeController : Controller
{
    protected IMunicipalityRepository MunicipalityRepository { get; set; }
    protected IPopulationFrameRepository PopulationFrameRepository { get; set; }

    public HomeController(IMunicipalityRepository municipalityRepository, IPopulationFrameRepository populationProgressRepository)
    {
        MunicipalityRepository = municipalityRepository;
        PopulationFrameRepository = populationProgressRepository;
    }

    [HttpGet]
    [Route("{district}/{name}/{code:municipalityCode}", Name = "MunicipalityRoute")]
    [Route("{controller=Home}/{action=Index}/{id?}")]
    public ViewResult Index(string? district, string? name, string? code)
    {
        var model = code == null ? CreateRandomModel() : CreateModelByCode(code);
        ViewData["CanonicalUrl"] = UrlUtils.CreateCanonicalUrl(model.Municipality);
        return View(model);
    }

    [HttpGet]
    [HttpPost]
    public IActionResult SelectMunicipality(string? municipalityName, string? currentMunicipalityCode)
    {
        if (string.IsNullOrEmpty(municipalityName))
        {
            var errorMessage = Resources.Controllers_HomeController.Municipality_name_is_required_;
            return CreateErrorResult(errorMessage, municipalityName, currentMunicipalityCode);
        }

        var municipalities = FindMunicipalitiesByNameWithDiscrict(municipalityName).ToArray();

        if (!municipalities.Any())
        {
            var errorMessage = Resources.Controllers_HomeController.No_municipality_found_;
            return CreateErrorResult(errorMessage, municipalityName, currentMunicipalityCode);
        }

        var municipality = municipalities.OrderBy(m => m.Name).First();
        var district = municipality.DistrictName.ToUrlSegment();
        var name = municipality.Name.ToUrlSegment();
        return RedirectToAction(nameof(Index), new { district = district, name = name, code = municipality.MunicipalityId });
    }

    [HttpGet]
    [Route("api/municipalities")]
    public ActionResult<string[]> FindMunicipalities(string name)
    {
        var municipalities = FindMunicipalitiesByNameWithDiscrict(name);
        var data = municipalities
            .OrderBy(m => m.Name)
            .ThenBy(m => m.DistrictName)
            .Select(m => $"{m.Name}, {m.DistrictName}").ToArray();

        return Ok(data);
    }

    private ActionResult CreateErrorResult(string errorMessage, string? municipalityName, string? currentMunicipalityCode)
    {
        var model = currentMunicipalityCode != null ? CreateModelByCode(currentMunicipalityCode) : CreateRandomModel();
        model.MunicipalityNameSearch = municipalityName ?? "";

        ModelState.AddModelError(nameof(model.MunicipalityNameSearch), errorMessage);
        
        var view = View(nameof(Index), model);
        view.StatusCode = StatusCodes.Status400BadRequest;
        return view;
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
        return CreateModelByMunicipality(municipality);
    }

    private MunicipalityPopulationProgressModel CreateModelByMunicipality(Municipality municipality)
    {
        var populationProgress = PopulationFrameRepository.FindAll(new QueryPopulationFrameByMunicipalityCode { Code = municipality.MunicipalityId });
        var closest = MunicipalityRepository.GetClosests(municipality.Longitude, municipality.Latitude);

        return new MunicipalityPopulationProgressModel
        {
            Municipality = municipality,
            PopulationProgress = populationProgress.OrderByDescending(d => d.Year).ToArray(),
            ClosestMunicipalities = closest.Skip(1).ToArray()
        };
    }

    private MunicipalityPopulationProgressModel CreateRandomModel()
    {
        var municipality = MunicipalityRepository.GetRandom();
        return CreateModelByMunicipality(municipality);
    }

    [Route("aplikace")]
    public IActionResult About()
    {
        return View();
    }

    [Route("error")]
    public IActionResult Error()
    {
        return View();
    }

    [Route("pagenotfound")]
    public IActionResult PageNotFound()
    {
        return View();
    }
}
