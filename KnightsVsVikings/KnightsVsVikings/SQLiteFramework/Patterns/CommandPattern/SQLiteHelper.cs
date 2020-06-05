﻿using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Interfaces.Patterns.CommandPattern;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern.SQLCommands;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern
{
    public static class SQLiteHelper
    {
        private static InsertRowCommand insertCommand = new InsertRowCommand();
        private static DeleteRowCommand deleteCommand = new DeleteRowCommand();
        private static GetRowCommand getCommand = new GetRowCommand();
        private static RenameTableCommand renameCommand = new RenameTableCommand();
        private static UpdateRowCommand updateCommand = new UpdateRowCommand();


        // Insert Command --
        public static ISQLiteRow Insert(this ISQLiteTable table, ISQLiteRow row)
        {
            return InsertRow(table, row, false);
        }

        public static ISQLiteRow Insert(this ISQLiteTable table, ISQLiteRow row, bool unique)
        {
            return InsertRow(table, row, unique);
        }
        // -- Insert Command

        // Delete Command --
        public static void Delete(this ISQLiteTable table, int id)
        {
            DeleteRow(table, typeof(SQLiteRowBase).GetProperty("Id"), id);
        }

        public static void Delete(this ISQLiteTable table, PropertyInfo property, object data)
        {
            DeleteRow(table, property, data);
        }
        // -- Delete Command

        // Get Command --
        public static ISQLiteRow Get(this ISQLiteTable table, int id)
        {
            return GetRow(table, typeof(SQLiteRowBase).GetProperty("Id"), id).First();
        }

        public static ISQLiteRow Get(this ISQLiteTable table, PropertyInfo property, object data)
        {
            return GetRow(table, property, data).First();
        }

        public static List<ISQLiteRow> GetMultiple(this ISQLiteTable table, PropertyInfo property, object data)
        {
            return GetRow(table, property, data);
        }

        public static List<ISQLiteRow> GetAll(this ISQLiteTable table)
        {
            return GetRow(table, null, null);
        }

        // -- Get Command

        // Rename Command --
        public static void Rename(this ISQLiteTable table, string renameTo)
        {
            renameCommand.ExecuteOnTable = table;
            renameCommand.NewTableName = renameTo;

            renameCommand.Execute();
        }
        // -- Rename Command

        // Update Command --
        public static void Update(this ISQLiteTable table, params KeyValuePair<PropertyInfo, object>[] updatedData)
        {
            UpdateRow(table, null, updatedData.ToDictionary(pair => pair.Key, pair => pair.Value));
        }

        public static void Update(this ISQLiteTable table, int id, params KeyValuePair<PropertyInfo, object>[] updatedData)
        {
            int[] idToArray = new int[] { id };

            UpdateRow(table, idToArray, updatedData.ToDictionary(pair => pair.Key, pair => pair.Value));
        }

        public static void Update(this ISQLiteTable table, int[] ids, params KeyValuePair<PropertyInfo, object>[] updatedData)
        {
            UpdateRow(table, ids, updatedData.ToDictionary(pair => pair.Key, pair => pair.Value));
        }
        // -- Update Command

        // Command Methods --
        private static ISQLiteRow InsertRow(ISQLiteTable table, ISQLiteRow row, bool unique)
        {
            insertCommand.ExecuteOnTable = table;
            insertCommand.IsUnique = unique;
            insertCommand.RowToInsert = row;

            insertCommand.Execute();

            return row;
        }

        private static void DeleteRow(ISQLiteTable table, PropertyInfo property, object data)
        {
            deleteCommand.Column = property;
            deleteCommand.Data = data;

            deleteCommand.ExecuteOnTable = table;

            deleteCommand.Execute();
        }

        private static List<ISQLiteRow> GetRow(ISQLiteTable table, PropertyInfo property, object data)
        {
            getCommand.Column = property;
            getCommand.Data = data;

            getCommand.ExecuteOnTable = table;

            getCommand.Execute();

            return getCommand.ResultRows;
        }

        private static void UpdateRow(ISQLiteTable table, int[] ids, Dictionary<PropertyInfo, object> updatedData)
        {
            updateCommand.ExecuteOnTable = table;
            updateCommand.Ids = ids;
            updateCommand.UpdatedData = updatedData;

            updateCommand.Execute();
        }
        // -- Command Methods
    }
}