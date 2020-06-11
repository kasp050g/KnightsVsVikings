using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern
{
    // Lucas

    interface ISQLiteOutput
    {
        List<ISQLiteRow> ResultRows { get; }
    }
}
