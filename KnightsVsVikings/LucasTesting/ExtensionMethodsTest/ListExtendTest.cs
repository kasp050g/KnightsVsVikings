using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.ExtensionMethodsTest
{
    public static class ListExtendTest
    {
        public static bool IsEqualsToList<TValue>(this List<TValue> values, List<TValue> valuesCompare)
        {
            bool result = false;

            if (values.Count == valuesCompare.Count)
                for (int i = 0; i < values.Count; i++)
                    if (values.ElementAt(i).Equals(valuesCompare.ElementAt(i)))
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
