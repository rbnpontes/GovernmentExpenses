﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GovernmentExpenses.Core
{
    public static class LingExtensions
    {
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> data, Func<TSource, TKey> predicate)
        {
            HashSet<TKey> sources = new HashSet<TKey>();
            foreach (TSource item in data)
            {
                var key = predicate(item);
                if (sources.Contains(key))
                    continue;
                sources.Add(key);
                yield return item;
            }
        }
    }
}
