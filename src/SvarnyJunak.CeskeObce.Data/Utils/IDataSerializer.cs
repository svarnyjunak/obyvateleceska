namespace SvarnyJunak.CeskeObce.Data.Utils
{
    public interface IDataSerializer
    {
        string GetDataExtension();

        string Write<T>(T data);
        T Read<T>(string data);
    }
}