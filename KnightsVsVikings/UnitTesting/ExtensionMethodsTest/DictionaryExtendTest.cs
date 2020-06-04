using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.ExtensionMethodsTest
{
    public static class DictionaryExtendTest
    {
        public static bool IsEqualsToDictionary<TKey, TValue>(this Dictionary<TKey, TValue> pairs, Dictionary<TKey, TValue> pairsCompare)
        {
            bool result = false;

            foreach (KeyValuePair<TKey, TValue> pair in pairs)
                foreach (KeyValuePair<TKey, TValue> pairCompare in pairsCompare)
                    if (pair.Equals(pairCompare))
                        result = true;
                    else
                    {
                        result = false;
                        break;
                    }

            return result;
        }
    }
}
