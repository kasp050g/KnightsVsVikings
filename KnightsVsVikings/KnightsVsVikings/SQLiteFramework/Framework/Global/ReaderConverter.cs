using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Framework.Global
{
    public static class ReaderConverter
    {
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

                default:
                    return reader.GetValue(index);
            }
        }
    }
}
