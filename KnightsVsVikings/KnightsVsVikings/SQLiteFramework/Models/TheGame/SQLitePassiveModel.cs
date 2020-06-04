using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    class SQLitePassiveModel : SQLiteRowBase
    {
        public int PercentId { get; set; }
        public int FlatId { get; set; }
        public float BuildTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DisplayImageName { get; set; }
        public string DisplayIconName { get; set; }

        public SQLitePassiveModel(ISQLiteTable locatedInTable, int percentId, int flatId, float buildTime, string name, string description, string displayImageName, string displayIconName) : base(locatedInTable)
        {
            PercentId = percentId;
            FlatId = flatId;
            BuildTime = buildTime;
            Name = name;
            Description = description;
            DisplayImageName = displayImageName;
            DisplayIconName = displayIconName;
        }
    }
}
