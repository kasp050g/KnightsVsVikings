using KnightsVsVikings.SQLiteFramework.Framework.Global;
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
    // Lucas

    /// <summary>
    /// Extension method to SQLiteTable.
    /// </summary>
    public static class SQLiteHelper
    {
        private static InsertRowCommand insertCommand = new InsertRowCommand();
        private static InsertMultipleRowsCommand insertMultipleCommand = new InsertMultipleRowsCommand();
        private static DeleteRowCommand deleteCommand = new DeleteRowCommand();
        private static GetRowCommand getCommand = new GetRowCommand();
        private static RenameTableCommand renameCommand = new RenameTableCommand();
        private static UpdateRowCommand updateCommand = new UpdateRowCommand();


        // Insert Command --
        /// <summary>
        /// Inserts a row into a SQLite table.
        /// </summary>
        /// <param name="table">Table to insert row to.</param>
        /// <param name="row">Row to insert.</param>
        /// <returns>Returns row.</returns>
        public static ISQLiteRow Insert(this ISQLiteTable table, ISQLiteRow row)
        {
            return InsertRow(table, row, false);
        }

        /// <summary>
        /// Inserts a row into a SQLite table.
        /// </summary>
        /// <param name="table">Table to insert row to.</param>
        /// <param name="row">Row to insert.</param>
        /// <param name="unique">Defines if the row is unique or not.</param>
        /// <returns>Returns row.</returns>
        public static ISQLiteRow Insert(this ISQLiteTable table, ISQLiteRow row, bool unique)
        {
            return InsertRow(table, row, unique);
        }
        // -- Insert Command

        // Insert Multiple Command --
        /// <summary>
        /// Inserts a list of rows into a SQLite table.
        /// </summary>
        /// <param name="table">Table to insert rows to.</param>
        /// <param name="rows">List of rows to insert.</param>
        public static void InsertMultiple(this ISQLiteTable table, List<ISQLiteRow> rows)
        {
            insertMultipleCommand.ExecuteOnTable = table;

            insertMultipleCommand.ResultRows = rows;

            insertMultipleCommand.Execute();
        }
        // -- Insert Multiple Command

        // Delete Command --
        /// <summary>
        /// Deletes row from SQLite table.
        /// </summary>
        /// <param name="table">Table to delete from.</param>
        /// <param name="id">Delete by Id.</param>
        public static void Delete(this ISQLiteTable table, int id)
        {
            DeleteRow(table, typeof(SQLiteRowBase).GetProperty("Id"), id);
        }

        /// <summary>
        /// Deletes row from SQLite table.
        /// </summary>
        /// <param name="table">Table to delete from.</param>
        /// <param name="property">Search in property/column</param>
        /// <param name="data">Search for data.</param>
        public static void Delete(this ISQLiteTable table, PropertyInfo property, object data)
        {
            DeleteRow(table, property, data);
        }
        // -- Delete Command

        // Get Command --
        /// <summary>
        /// Gets a row from a SQLite table.
        /// </summary>
        /// <param name="table">Table to get row from.</param>
        /// <param name="id">Id to find.</param>
        /// <returns>Returns found row.</returns>
        public static ISQLiteRow Get(this ISQLiteTable table, int id)
        {
            return GetRow(table, typeof(SQLiteRowBase).GetProperty("Id"), id).First();
        }

        /// <summary>
        /// Gets a row from a SQLite table.
        /// </summary>
        /// <param name="table">Table to get row from.</param>
        /// <param name="property">Search in property/column</param>
        /// <param name="data">Search for data.</param>
        /// <returns>Returns found row.</returns>
        public static ISQLiteRow Get(this ISQLiteTable table, PropertyInfo property, object data)
        {
            return GetRow(table, property, data).First();
        }

        /// <summary>
        /// Gets multiple rows from a SQLite table.
        /// </summary>
        /// <param name="table">Table to get row from.</param>
        /// <param name="property">Search in property/column</param>
        /// <param name="data">Search for data.</param>
        /// <returns>Returns list of found rows.</returns>
        public static List<ISQLiteRow> GetMultiple(this ISQLiteTable table, PropertyInfo property, object data)
        {
            return GetRow(table, property, data);
        }

        /// <summary>
        /// Get all rows from a SQLite table.
        /// </summary>
        /// <param name="table">Table to get rows from.</param>
        /// <returns>Returns a list of all rows.</returns>
        public static List<ISQLiteRow> GetAll(this ISQLiteTable table)
        {
            return GetRow(table, null, null);
        }

        // -- Get Command

        // Rename Command --
        /// <summary>
        /// Renames a SQLite table name.
        /// </summary>
        /// <param name="table">Table to rename.</param>
        /// <param name="renameTo">Name to change to.</param>
        public static void Rename(this ISQLiteTable table, string renameTo)
        {
            renameCommand.ExecuteOnTable = table;
            renameCommand.NewTableName = renameTo;

            renameCommand.Execute();
        }
        // -- Rename Command

        // Update Command --
        /// <summary>
        /// Updates a row in a SQLite table.
        /// </summary>
        /// <param name="table">Table to update in.</param>
        /// <param name="updatedData">Updates all rows at the given properties/columns with the paired value.</param>
        public static void Update(this ISQLiteTable table, params KeyValuePair<PropertyInfo, object>[] updatedData)
        {
            UpdateRow(table, null, updatedData.ToDictionary(pair => pair.Key, pair => pair.Value));
        }

        /// <summary>
        /// Updates a row in a SQLite table.
        /// </summary>
        /// <param name="table">Table to update in.</param>
        /// <param name="id">Id of the row to update.</param>
        /// <param name="updatedData">Updates found row with the given properties/columns with the paired value.</param>
        public static void Update(this ISQLiteTable table, int id, params KeyValuePair<PropertyInfo, object>[] updatedData)
        {
            int[] idToArray = new int[] { id };

            UpdateRow(table, idToArray, updatedData.ToDictionary(pair => pair.Key, pair => pair.Value));
        }

        /// <summary>
        /// Updates a row in a SQLite table.
        /// </summary>
        /// <param name="table">Table to update in.</param>
        /// <param name="ids">Ids of rows to update.</param>
        /// <param name="updatedData">Updates all found rows with the given properties/columns with the paired value.</param>
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
