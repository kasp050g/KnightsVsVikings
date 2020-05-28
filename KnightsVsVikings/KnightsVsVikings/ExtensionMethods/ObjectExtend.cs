using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.ExtensionMethods
{
    public static class ObjectExtend
    {
        public static object ObjectToSQLiteString(this object input)
        {
            if (input is string)
                return $"'{input}'";
            else
                return input;
        }
    }
}
