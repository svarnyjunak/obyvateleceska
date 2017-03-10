using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data.Entities;
using SvarnyJunak.CeskeObce.Data.Utils;

namespace SvarnyJunak.CeskeObce.DataParser
{
    public class SiteMapBuilder
    {
        private const int MAX_RECORDS_COUNT_IN_SITEMAP_FOR_GOOGLE = 50000;
        public string Build(string websiteUrl,IEnumerable<Municipality> municipalities)
        {
            if(municipalities.Count() >= MAX_RECORDS_COUNT_IN_SITEMAP_FOR_GOOGLE)
                throw new ArgumentException("Too many records to generate sitemap.");

            var siteMapStart = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">";
            var pagePart = "<url><loc>{0}/{1}/{2}/{3}</loc></url>";
            var siteMapEnd = "</urlset>";

            var sb = new StringBuilder();
            sb.AppendLine(siteMapStart);

            foreach (var municipality in municipalities)
            {
                sb.AppendLine(String.Format(
                    pagePart,
                    websiteUrl,
                    municipality.DistrictName.PrepareForUrl(),
                    municipality.Name.PrepareForUrl(),
                    municipality.Code));
            }

            sb.AppendLine(siteMapEnd);

            return sb.ToString();
        }
    }
}
