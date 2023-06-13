using SvarnyJunak.CeskeObce.DataParser.Entities;
using SvarnyJunak.CeskeObce.DataParser.Utils;

namespace SvarnyJunak.CeskeObce.DataParser.Parsers
{
    public abstract class ParserBase
    {
        public ParserBase()
        {
            Parser = new DataRowParser();
        }

        public DataRowParser Parser { get; set; }
    }
}