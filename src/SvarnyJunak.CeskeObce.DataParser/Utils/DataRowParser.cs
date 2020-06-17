using System;

namespace SvarnyJunak.CeskeObce.DataParser.Utils
{
    public class DataRowParser
    {
        public int ParseInt(object value)
        {
            return Convert.ToInt32(value);
        }

        public decimal ParseDecimal(object value)
        {
            return Convert.ToDecimal(value);
        }

        public string ParseString(object value)
        {
            if (value == null)
                return String.Empty;

            return value.ToString()!;
        }

        public double ParseDouble(object value)
        {
            return Convert.ToDouble(value);
        }
    }
}
