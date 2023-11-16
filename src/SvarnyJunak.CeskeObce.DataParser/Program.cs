using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.DataParser;
using Microsoft.Extensions.DependencyInjection;
using SvarnyJunak.CeskeObce.Data;

public class Program
{
    private static ServiceProvider _serviceProvider = null!;

    static async Task Main(string[] args)
    {
        RegisterServices();

        IServiceScope scope = _serviceProvider.CreateScope();
        await scope.ServiceProvider.GetRequiredService<ParserRunner>().Run();

        DisposeServices();
    }

    private static void RegisterServices()
    {
        var services = new ServiceCollection();

        services.AddTransient<ParserRunner>();
        services.AddTransient<IMunicipalityRepository, MunicipalityRepository>();
        services.AddTransient<IPopulationFrameRepository, PopulationFrameRepository>();
        services.UseFileRepositories(GetDataPath());

        _serviceProvider = services.BuildServiceProvider(true);
    }

    private static string GetDataPath()
    {
        var directory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.GetDirectories("SvarnyJunak.CeskeObce.Web").Single();
        return Path.Combine(directory.FullName, "appdata");
    }

    private static void DisposeServices()
    {
        _serviceProvider?.Dispose();
    }
}