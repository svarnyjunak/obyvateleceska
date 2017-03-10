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

        /// <summary>
        /// Replaces all spaces by '-' and all text is converted to lowercase.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string PrepareForUrl(this string text)
        {
            return text.ToLower().Replace(" ", "-");
        }

        /// <summary>
        /// Replaces all '-' by spaces.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string PrepareFromUrl(this string text)
        {
            return text.Replace("-", " ");
        }
    }
}
