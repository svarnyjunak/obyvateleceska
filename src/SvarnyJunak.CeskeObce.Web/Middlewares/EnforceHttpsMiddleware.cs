using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Web.Middlewares
{
    public class EnforceHttpsMiddleware
    {
        private readonly RequestDelegate _next;

        public EnforceHttpsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpRequest req = context.Request;

            if (req.Path.HasValue && req.Path.Value.Contains(".well-known"))
            {
                await _next(context); //ověření domény - nepřesměrovávat 
            }

            if (req.IsHttps == false)
            {
                string url = "https://" + req.Host + req.Path + req.QueryString;
                context.Response.Redirect(url, permanent: true);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
