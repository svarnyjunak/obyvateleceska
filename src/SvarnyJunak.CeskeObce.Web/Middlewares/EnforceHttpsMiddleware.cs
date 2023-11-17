namespace SvarnyJunak.CeskeObce.Web.Middlewares;

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
            await _next(context); // Domain name check - do not redirect.
        }
        else if (req.IsHttps == false)
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
