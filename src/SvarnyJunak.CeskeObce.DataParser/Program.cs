using System;
using SvarnyJunak.CeskeObce.DataParser;
using SvarnyJunak.CeskeObce.Data.Repositories.SerializedJson;
using SvarnyJunak.CeskeObce.Data.Utils;

class Program
{
    static void Main(string[] args)
    {
        var jsonDataSerializer = new JsonDataSerializer();
        var storer = new JsonDataStorage(jsonDataSerializer, "C:\\Temp\\");
        var runner = new ParserRunner(storer);

        runner.Run();
    }
}