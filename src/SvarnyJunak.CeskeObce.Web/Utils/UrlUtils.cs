using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Web.Utils
{
    public static class UrlUtils
    {
        public static string CreateCanonicalUrl(Municipality municipality)
        {
            return $"https://obyvateleceska.cz{CreateRelativeUrl(municipality)}";
        }

        public static string CreateRelativeUrl(Municipality municipality)
        {
            var district = municipality.DistrictName.ToUrlSegment();
            var name = municipality.Name.ToUrlSegment();
            return $"/{district}/{name}/{municipality.MunicipalityId}";
        }
    }
}
