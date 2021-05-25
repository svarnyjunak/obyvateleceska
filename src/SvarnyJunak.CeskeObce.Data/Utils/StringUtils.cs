using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Data.Utils
{
    public static class StringUtils
    {
        public static string RemoveDiacritics(this string text)
        {
            string stringFormD = text.Normalize(NormalizationForm.FormD);
            var retVal = new StringBuilder();
            foreach (char ch in stringFormD)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                    retVal.Append(ch);
            }
            return retVal.ToString().Normalize(NormalizationForm.FormC);
        }

        public static bool HasDiacritics(this string text)
        {
            string stringFormD = text.Normalize(NormalizationForm.FormD);
            return stringFormD.Any(ch => CharUnicodeInfo.GetUnicodeCategory(ch) == UnicodeCategory.NonSpacingMark);
        }

        public static bool CompareWithoutDiacriticsIfNotProvided(this string a, string b)
        {
            var aUpperCased = a.ToUpper().Trim();
            var bUpperCased = b.ToUpper().Trim();

            if (a.HasDiacritics())
                return bUpperCased.StartsWith(aUpperCased);

            return bUpperCased.RemoveDiacritics().StartsWith(aUpperCased.RemoveDiacritics());
        }

        public static string ToUrlSegment(this string text)
        {
            return text.RemoveDiacritics().Trim().ToLower().Replace(' ', '-');
        }
    }
}
