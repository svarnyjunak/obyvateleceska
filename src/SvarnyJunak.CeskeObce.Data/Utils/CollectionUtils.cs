using System;
using System.Linq;

namespace SvarnyJunak.CeskeObce.Data.Utils
{
    public static class CollectionUtils
    {
        private static readonly Random _random = new Random();

        public static T GetRandomElement<T>(this IQueryable<T> list)
        {
            var count = list.Count();

            if (count == 0)
            {
                return default(T) ?? throw new InvalidOperationException();
            }

            var random = _random.Next(count);
            return list.Skip(random - 1).Take(1).Single();
        }
    }
}
