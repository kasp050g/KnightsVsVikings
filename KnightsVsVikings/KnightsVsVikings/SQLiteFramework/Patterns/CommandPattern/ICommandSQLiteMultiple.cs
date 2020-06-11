using KnightsVsVikings.Script.TheGame.Patterns.CommandPattern;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern
{
    // Lucas
    interface ICommandSQLiteMultiple : ICommand
    {
        ISQLiteTable[] ExecuteOnTables { get; set; }
    }
}
