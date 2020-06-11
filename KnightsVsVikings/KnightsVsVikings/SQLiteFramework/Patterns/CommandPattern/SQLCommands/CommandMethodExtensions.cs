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
    // Lucas
    public static class CommandMethodExtensions
    {
        /// <summary>
        /// Converts all properties to string.
        /// </summary>
        /// <param name="row">Row to convert from.</param>
        /// <returns>Returns joined string.</returns>
        public static string GetPropertiesToString(this ISQLiteRow row)
        {
            List<string> result = new List<string>();

            // Først findes alle properties.
            List<PropertyInfo> properties = row.GetType().GetProperties().ToList();

            // Herefter slettes alle properties der ikke er relevante. F.eks.: locatedInTable.
            properties = properties.RemoveAllBaseProperties();

            // Herefter bliver navne på Properties'ne tilføjet til en liste af string.
            foreach (PropertyInfo property in properties)
                result.Add(property.Name);

            return string.Join(", ", result);
        }

        /// <summary>
        /// Converts all property values to a string.
        /// </summary>
        /// <param name="row">Row to convert from.</param>
        /// <returns>Returns joined string.</returns>
        public static string GetValuesToString(this ISQLiteRow row)
        {
            List<string> result = new List<string>();

            // Først findes alle properties.
            List<PropertyInfo> properties = row.GetType().GetProperties().ToList();

            // Herefter slettes alle properties der ikke er relevante. F.eks.: locatedInTable.
            properties = properties.RemoveAllBaseProperties();

            // Herefter bliver værdierne på Properties'ne tilføjet til en liste af string.
            foreach (PropertyInfo property in properties)
                result.Add(Convert.ToString(property.GetValue(row).ObjectToSQLiteString()));

            return string.Join(", ", result);
        }

        /// <summary>
        /// Removes all properties from the SQLiteRowBase class from SQLite models, except for the Id property.
        /// </summary>
        /// <param name="properties">Properties to remove from.</param>
        /// <returns>Returns cleaned list of properties.</returns>
        public static List<PropertyInfo> RemoveBaseProperties(this List<PropertyInfo> properties)
        {
            List<PropertyInfo> baseProperties = typeof(SQLiteRowBase).GetProperties().Where(property => property.Name != "Id").ToList();

            properties.RemoveAll(property => baseProperties.Exists(baseProperty => baseProperty.Name == property.Name));

            return properties.OrderBy(property => property.Name != "Id").ToList();
        }

        /// <summary>
        /// Removes all properties from the SQLiteRowBase class from SQLite models.
        /// </summary>
        /// <param name="properties">Properties to remove from.</param>
        /// <returns>Returns cleaned list of properties.</returns>
        public static List<PropertyInfo> RemoveAllBaseProperties(this List<PropertyInfo> properties)
        {
            List<PropertyInfo> baseProperties = typeof(SQLiteRowBase).GetProperties().ToList();

            properties.RemoveAll(property => baseProperties.Exists(baseProperty => baseProperty.Name == property.Name));

            return properties.AsEnumerable().OrderBy(property => property.Name != "Id").ToList();
        }
    }
}
