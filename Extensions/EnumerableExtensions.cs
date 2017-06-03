using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryAnalyzer
{
    public static class EnumerableExtensions
    {
        public static bool ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            var enumerable = list as IList<T> ?? list.ToList();
            foreach (var item in enumerable)
            {
                action(item);
            }
            return enumerable.Any();
        }
    }
}