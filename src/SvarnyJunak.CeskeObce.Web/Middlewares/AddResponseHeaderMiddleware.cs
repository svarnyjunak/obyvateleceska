using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Web.Middlewares;

public class AddResponseHeaderMiddleware
{
    private readonly RequestDelegate _next;
    private string _headerName;
    private StringValues _headerValues;

    public AddResponseHeaderMiddleware(RequestDelegate next, string headerName, StringValues headerValues)
    {
        _next = next;
        _headerName = headerName;
        _headerValues = headerValues;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.OnStarting(SetHeader, context);
        await _next(context);
    }

    private Task SetHeader(object context)
    {
        var httpContext = (HttpContext)context;
        var responseHeaders = httpContext.Response.Headers;

        if (responseHeaders.Keys.All(k => k != _headerName))
        {
            responseHeaders.Append(_headerName, _headerValues);
        }

        return Task.FromResult(0);
    }
}
