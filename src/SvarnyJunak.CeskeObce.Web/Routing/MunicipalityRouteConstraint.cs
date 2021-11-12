using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Repositories;

namespace SvarnyJunak.CeskeObce.Web.Routing;

public class MunicipalityRouteConstraint : IRouteConstraint
{
    private readonly IServiceProvider _serviceProvider;
    private static Municipality[]? _municipalities = null;

    public MunicipalityRouteConstraint(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (!values.ContainsKey(routeKey))
            return false;

        return GetCachedData().Any(m => m.MunicipalityId == (string?)values[routeKey]);
    }

    private Municipality[] GetCachedData()
    {
        if (_municipalities == null)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IMunicipalityRepository>();

                _municipalities = repository.FindAll().ToArray();
            }

        }

        return _municipalities;
    }
}
