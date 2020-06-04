using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern.SQLCommands
{
    public static class CommandMethodExtensions
    {
        public static string GetPropertiesToString(this ISQLiteRow row)
        {
            List<string> result = new List<string>();

            PropertyInfo[] properties = row.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
                result.Add(property.Name);

            return string.Join(", ", result);
        }

        public static string GetValuesToString(this ISQLiteRow row)
        {
            List<string> result = new List<string>();

            PropertyInfo[] properties = row.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
                result.Add((string)property.GetValue(row).ObjectToSQLiteString());

            return string.Join(", ", result);
        }
    }
}
