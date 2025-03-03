using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public static class LINQHashTable
    {
        public static IEnumerable<T> WhereCustom<T>(this IEnumerable<T> collection, Func<T, bool> condition)
        {
            foreach (var item in collection)
            {
                if (condition(item))
                {
                    yield return item;
                }
            }
        }
        public static double AggregateCustom<T>(this IEnumerable<T> collection, Func<T, double> selector, Func<double, double, double> operation) 
        {
            double result = 0;
            foreach (var item in collection) 
            {
                result = operation(result, selector(item));
            }
            return result;
        }
        public static IEnumerable<T> OrderByCustom<T, Tkey>(this IEnumerable<T> collection, Func<T, Tkey> keySelector, bool descending = false) 
        {
            return descending ? collection.OrderByDescending(keySelector) : collection.OrderBy(keySelector);
        }
    }
}
