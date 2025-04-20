using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIerarchyLib
{
    public static class LINQHashTable
    {
        public static IEnumerable<KeyValuePair<TKey, TVal>> WhereCustom<TKey, TVal>(this NewCustomHashTable<TKey, TVal> collection, Func<KeyValuePair<TKey, TVal>, bool> condition)
        {
            foreach (var item in collection)
            {
                if (condition(item))
                {
                    yield return item;
                }
            }
        }
        public static double AggregateCustom<TKey, TVal>(this NewCustomHashTable<TKey, TVal> collection, Func<KeyValuePair<TKey, TVal>, double> selector, Func<double, double, double> operation) 
        {
            double result = 0;
            foreach (var item in collection) 
            {
                result = operation(result, selector(item));
            }
            return result;
        }
        public static IEnumerable<KeyValuePair<TKey, TVal>> OrderByCustom<TKey, TVal, TKeySort>(this NewCustomHashTable<TKey, TVal> collection, Func<KeyValuePair<TKey, TVal>, TKeySort> keySelector, bool descending = false)
        {
            return descending ? collection.OrderByDescending(keySelector) : collection.OrderBy(keySelector);
        }
    }
}