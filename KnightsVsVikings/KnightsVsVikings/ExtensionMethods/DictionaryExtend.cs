using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.ExtensionMethods.Dictionary
{
    public static class DictionaryExtend
    {
        public static void RemoveRange<TKey, TValue>(this Dictionary<TKey, TValue> pairs, Dictionary<TKey, TValue> pairsToRemove)
        {
            List<TKey> deleteFrom = new List<TKey>();

            foreach (KeyValuePair<TKey, TValue> pair in pairs)
                if (pairsToRemove.Contains(pair))
                    deleteFrom.Add(pair.Key);

            foreach (TKey key in deleteFrom)
                pairs.Remove(key);

        }

        public static string DictToSQLiteString<TKey, TValue>(this Dictionary<TKey, TValue> pairs)
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<TKey, TValue> pair in pairs)
                result.Add($"{pair.Key} {(pair.Value as Type).TypeToSQLiteType()}");

            return string.Join(", ", result);
        }
    }
}
