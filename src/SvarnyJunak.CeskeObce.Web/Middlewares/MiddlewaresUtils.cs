using Microsoft.Extensions.Primitives;
using System.Text;

namespace SvarnyJunak.CeskeObce.Web.Middlewares;

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
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMiddleware<AddResponseHeaderMiddleware>("X-Content-Type-Options", new StringValues("nosniff"));
    }

    public static IApplicationBuilder UseXssProtectionHeader(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMiddleware<AddResponseHeaderMiddleware>("X-XSS-Protection", new StringValues("1; mode=block"));
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
                "https://api.mapy.cz",
                "https://www.google-analytics.com/analytics.js",
                "https://pagead2.googlesyndication.com",
                "https://adservice.google.cz/adsid/integrator.js",
                "https://adservice.google.com/adsid/integrator.js"
            };

        var img = new[]
        {
                "'self'",
                "https://csi.gstatic.com",
                "https://www.google-analytics.com",
                "https://*.googleapis.com",
                "http://api.mapy.cz",
                "http://mapserver.mapy.cz",
                "https://stats.g.doubleclick.net",
            };

        var sb = new StringBuilder();
        sb.Append(GetCspRule("script-src", js));
        sb.Append(GetCspRule("img-src", img));
        sb.Append("object-src 'self'; ");

        return app.UseMiddleware<AddResponseHeaderMiddleware>("Content-Security-Policy", new StringValues(sb.ToString()));
    }
}
