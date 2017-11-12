using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
