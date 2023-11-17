using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SvarnyJunak.CeskeObce.Web.Middlewares;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SvarnyJunak.CeskeObce.Web.Test.Middlewares
{
    public class EnforceHttpsMiddlewareTests
    {
        [Fact]
        public async Task NonHttpCallIsRedirected()
        {
            // Arrange
            using var http = await CreateHttpClientAsync();

            // Act
            var response = await http.GetAsync("http://localhost/");

            // Assert
            Assert.Equal(HttpStatusCode.MovedPermanently, response.StatusCode);
        }

        [Fact]
        public async Task HttpsCallIsProcessed()
        {
            // Arrange
            using var http = await CreateHttpClientAsync();

            // Act
            var response = await http.GetAsync("https://localhost/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task HttpCallToWellKnownEndpointIsProcessed()
        {
            // Arrange
            using var http = await CreateHttpClientAsync();

            // Act
            var response = await http.GetAsync("http://localhost/.well-known/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private static async Task<HttpClient> CreateHttpClientAsync()
        {
            var host = await new HostBuilder()
               .ConfigureWebHost(webBuilder =>
               {
                   webBuilder
                       .UseTestServer()
                       .ConfigureServices(services =>
                       {
                           services.AddRouting();
                       })
                       .Configure(app =>
                       {
                           app.UseMiddleware<EnforceHttpsMiddleware>();
                           app.UseRouting();
                           app.UseEndpoints(endpoints =>
                           {
                               endpoints.MapGet("/", () => "OK");
                               endpoints.MapGet("/.well-known", () => "OK");
                           });
                       });
               })
               .StartAsync();

            return host.GetTestClient();
        }

    }
}
