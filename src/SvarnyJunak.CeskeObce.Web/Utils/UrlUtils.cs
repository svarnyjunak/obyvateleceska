using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.Web.Utils
{
    public static class UrlUtils
    {
        public static string CreateCanonicalUrl(Municipality municipality)
        {
            var district = municipality.DistrictName.ToUrlSegment();
            var name = municipality.Name.ToUrlSegment();
            return $"https://obyvateleceska.cz/{district}/{name}/{municipality.MunicipalityId}";
        }
    }
}
