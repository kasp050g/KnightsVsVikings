using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.SQLiteFramework.Framework.Global;
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

            List<PropertyInfo> properties = row.GetType().GetProperties().ToList();

            properties = properties.RemoveAllBaseProperties();

            foreach (PropertyInfo property in properties)
                result.Add(property.Name);

            return string.Join(", ", result);
        }

        public static string GetValuesToString(this ISQLiteRow row)
        {
            List<string> result = new List<string>();

            List<PropertyInfo> properties = row.GetType().GetProperties().ToList();

            properties = properties.RemoveAllBaseProperties();

            foreach (PropertyInfo property in properties)
                result.Add(Convert.ToString(property.GetValue(row).ObjectToSQLiteString()));

            return string.Join(", ", result);
        }
        public static List<PropertyInfo> RemoveBaseProperties(this List<PropertyInfo> properties)
        {
            List<PropertyInfo> baseProperties = typeof(SQLiteRowBase).GetProperties().Where(property => property.Name != "Id").ToList();

            properties.RemoveAll(property => baseProperties.Exists(baseProperty => baseProperty.Name == property.Name));

            return properties.AsEnumerable().OrderBy(property => property.Name != "Id").ToList();
        }

        public static List<PropertyInfo> RemoveAllBaseProperties(this List<PropertyInfo> properties)
        {
            List<PropertyInfo> baseProperties = typeof(SQLiteRowBase).GetProperties().ToList();

            properties.RemoveAll(property => baseProperties.Exists(baseProperty => baseProperty.Name == property.Name));

            return properties.AsEnumerable().OrderBy(property => property.Name != "Id").ToList();
        }
    }
}
