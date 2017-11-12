using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Web.Middlewares
{
    public class ContentTypeOptionsNoSniffMiddleware
    {
        private readonly RequestDelegate _next;

        public ContentTypeOptionsNoSniffMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(state => {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("X-Content-Type-Options", new[] { "nosniff" });
                return Task.FromResult(0);
            }, context);

            await _next(context);
        }
    }
}
