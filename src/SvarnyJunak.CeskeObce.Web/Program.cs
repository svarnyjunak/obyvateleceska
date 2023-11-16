using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Net.Http.Headers;
using SvarnyJunak.CeskeObce.Data;
using SvarnyJunak.CeskeObce.Web.Middlewares;
using SvarnyJunak.CeskeObce.Web.Middlewares.ApplicationInsights;
using SvarnyJunak.CeskeObce.Web.Routing;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);
builder.Services.AddApplicationInsightsTelemetryProcessor<NotFoundFilter>();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.ConstraintMap.Add("municipalityCode", typeof(MunicipalityRouteConstraint));
});

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(180);
});

builder.Services
    .AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.UseFileRepositories(Path.Combine(Environment.CurrentDirectory, "appdata"));

var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");

    //todo: use app.UseHttpsRedirection();
    app.UseHttpsEnforcement();
    app.UseHsts();
}

//todo: app.UseCors();
app.UseContentTypeNoSniffHeader();

app.UseXssProtectionHeader();

// todo: app.UseCsp(...)
app.UseContentSecurityPolicyHeader();

// todo: use app.UseStatusCodePagesWithRedirects(...)
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/pagenotfound";
        await next();
    }
});

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24;
        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
    }
});
app.UseRouting();
app.MapControllers();
app.MapDefaultControllerRoute();

var supportedCultures = new[] { new CultureInfo("cs-CZ") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("cs-CZ"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.Run();