using KnightsVsVikings.Script.TheGame.Patterns.CommandPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Interfaces.Patterns.CommandPattern
{
    interface ICommandSQLiteSingle : ICommand
    {
        ISQLiteTable ExecuteOnTable { get; set; }
    }
}
