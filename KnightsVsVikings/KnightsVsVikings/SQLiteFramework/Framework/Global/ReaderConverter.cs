using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Framework.Global
{
    // Lucas
    public static class ReaderConverter
    {
        /// <summary>
        /// Assign GetType to IDataReader.
        /// </summary>
        /// <param name="reader">The reader to get from.</param>
        /// <param name="type">The Type to check.</param>
        /// <param name="index">The index of the element to return from the reader.</param>
        /// <returns></returns>
        public static object Get(this IDataReader reader, Type type, int index)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int64:
                    return reader.GetInt32(index);

                case TypeCode.Int32:
                    return reader.GetInt32(index);

                case TypeCode.String:
                    return reader.GetString(index);

                case TypeCode.Double:
                    return reader.GetFloat(index);

                case TypeCode.Single:
                    return reader.GetFloat(index);

                default:
                    return reader.GetValue(index);
            }
        }
    }
}
