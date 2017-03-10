using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Data.Utils
{
    public static class CollectionUtils
    {
        private static readonly Random random = new Random();

        public static T GetRandomElement<T>(this IEnumerable<T> list)
        {
            // If there are no elements in the collection, return the default value of T
            var array = list as T[] ?? list.ToArray();
            if (array.Length == 0)
                return default(T);

            return array[random.Next(array.Length)];
        }
    }
}
