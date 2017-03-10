using SvarnyJunak.CeskeObce.DataParser.Utils;

namespace SvarnyJunak.CeskeObce.DataParser
{
    public class ParserBase
    {
        public ParserBase()
        {
            Parser = new DataRowParser();
        }

        public DataRowParser Parser { get; set; }
    }
}