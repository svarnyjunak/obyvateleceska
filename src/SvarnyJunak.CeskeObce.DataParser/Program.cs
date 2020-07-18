using System;
using System.IO;
using System.Linq;
using FileContextCore.FileManager;
using FileContextCore.Serializer;
using SvarnyJunak.CeskeObce.Data.Repositories;
using SvarnyJunak.CeskeObce.DataParser;
using FileContextCore;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static ServiceProvider _serviceProvider;

    static void Main(string[] args)
    {
        RegisterServices();

        IServiceScope scope = _serviceProvider.CreateScope();
        scope.ServiceProvider.GetRequiredService<ParserRunner>().Run();

        DisposeServices();
    }

    private static void RegisterServices()
    {
        var services = new ServiceCollection();

        services.AddTransient<ParserRunner>();
        //services.AddTransient<IMunicipalityRepository, MunicipalityRepository>();
        //services.AddTransient<IPopulationFrameRepository, PopulationFrameRepository>();
        services.AddDbContext<CeskeObceDbContext>(options =>
        {
            options.UseFileContextDatabase<JSONSerializer, DefaultFileManager>(location: GetDataPath());
        });

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