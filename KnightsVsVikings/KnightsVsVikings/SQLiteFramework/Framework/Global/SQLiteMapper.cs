﻿using KnightsVsVikings.ExtensionMethods.Dictionary;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern.SQLCommands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Framework.Global
{
    class SQLiteMapper<T> : ISQLiteMapper where T : ISQLiteRow
    {
        public List<ISQLiteRow> MapRowsFromReader(IDataReader reader, ISQLiteTable readFromTable)
        {
            List<ISQLiteRow> result = new List<ISQLiteRow>();

            while(reader.Read())
            {
                ISQLiteRow row = CreateRow(reader);

                row.LocatedInTable = readFromTable;

                result.Add(row);
            }

            return result;
        }

        private List<dynamic> ReaderVariables(IDataReader reader, List<PropertyInfo> properties)
        {
            List<dynamic> result = new List<dynamic>();

            result.Add(reader.GetInt32(0)); // <= ID, Index, Identifier

            for (int i = 1; i < typeof(T).GetProperties().Length - 1; i++)
            {
                result.Add(reader.Get(properties.ElementAt(i).PropertyType, i));
            }
                //result.Add(reader.GetValue(i));

            return result;
        }

        //private List<PropertyInfo> RemoveBaseProperties(List<PropertyInfo> properties)
        //{
        //    List<PropertyInfo> baseProperties = typeof(SQLiteRowBase).GetProperties().Where(property => property.Name != "Id").ToList();
        //
        //    properties.RemoveAll(property => baseProperties.Exists(baseProperty => baseProperty.Name == property.Name));
        //
        //    return properties.AsEnumerable().OrderBy(property => property.Name != "Id").ToList();
        //}

        private ISQLiteRow CreateRow(IDataReader reader)
        {
            //ISQLiteRow row = (T)Activator.CreateInstance(typeof(T));
            T row = (T)Activator.CreateInstance(typeof(T));

            //List<PropertyInfo> properties = CommandMethodExtensions.RemoveBaseProperties(typeof(T).GetProperties().ToList());

            List<PropertyInfo> properties = typeof(T).GetProperties().ToList().RemoveBaseProperties();

            List<dynamic> values = ReaderVariables(reader, properties);

            for (int i = 0; i < properties.Count; i++)
                properties.ElementAt(i).SetValue(row, values[i]);

            return row;
        }
    }
}
