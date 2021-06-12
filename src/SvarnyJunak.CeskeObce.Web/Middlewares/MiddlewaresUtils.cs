using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Web.Middlewares
{
    public static class MiddlewaresUtils
    {
        public static IApplicationBuilder UseHttpsEnforcement(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<EnforceHttpsMiddleware>();
        }

        public static IApplicationBuilder UseContentTypeNoSniffHeader(this IApplicationBuilder app)
        {
            if(app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<AddResponseHeaderMiddleware>("X-Content-Type-Options", new StringValues("nosniff") );
        }

        public static IApplicationBuilder UseXssProtectionHeader(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<AddResponseHeaderMiddleware>("X-XSS-Protection", new StringValues("1; mode=block") );
        }

        private static string GetCspRule(string name, string[] values)
        {
            var sb = new StringBuilder();
            sb.Append(name);
            sb.Append(" ");
            sb.Append(String.Join(' ', values));
            sb.Append("; ");
            return sb.ToString();
        }

        public static IApplicationBuilder UseContentSecurityPolicyHeader(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var js = new[]
            {
                "'self'",
                "'unsafe-inline'", // google adsense
                "https://api.mapy.cz",
                //"https://maps.googleapis.com",
                //"https://maps.gstatic.com",
                "https://www.google-analytics.com/analytics.js",
                "https://az416426.vo.msecnd.net/scripts/a/ai.0.js",
                "https://pagead2.googlesyndication.com",
                "https://adservice.google.cz/adsid/integrator.js",
                "https://adservice.google.com/adsid/integrator.js"
             /* 
                "'sha256-XNzxjnKkNNDQIdgm47tH693jYB/vMQuAJD366bJnNVA='",
                "'sha256-gTNuTcADd7aFfQROeHc6OQsKqPlLON+shrmJUHeb+0E='",
                "'sha256-1Gl1aQj35BHBndlNA5NqZN/Yh2jaJx1U2IQEm7Cad1o='"
             */
            };

            var img = new[]
            {
                "'self'",
                "https://csi.gstatic.com",
                "https://www.google-analytics.com",
                "https://*.googleapis.com",
                "http://api.mapy.cz",
                "http://mapserver.mapy.cz",
//                "https://maps.googleapis.com",
//                "https://maps.gstatic.com",
                "https://stats.g.doubleclick.net",
            };

            var sb = new StringBuilder();
            sb.Append(GetCspRule("script-src", js));
            sb.Append(GetCspRule("img-src", img));
            sb.Append("object-src 'self'; ");            
            // sb.Append("style-src 'self' https://fonts.googleapis.com");

            return app.UseMiddleware<AddResponseHeaderMiddleware>("Content-Security-Policy", new StringValues(sb.ToString()));
        }
    }
}
