using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings.SQLiteFramework.Models.TheGame
{
    public class SQLiteStatsModel : SQLiteRowBase
    {
        public int FoodCost { get; set; }
        public int GoldCost { get; set; }
        public int WoodCost { get; set; }
        public int StoneCost { get; set; }
        public float BuildTime { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public float Speed { get; set; }
        public int Range { get; set; }
        public int GatheringAmount { get; set; }
        public float GatheringSpeed { get; set; }
        public int GatheringCapacity { get; set; }

        public SQLiteStatsModel(ISQLiteTable locatedInTable, int foodCost, int goldCost, int woodCost, int stoneCost, float buildTime, int health, int damage, int armor, float speed, int range, int gatheringAmount, float gatheringSpeed, int gatheringCapacity) : base(locatedInTable)
        {
            FoodCost = foodCost;
            GoldCost = goldCost;
            WoodCost = woodCost;
            StoneCost = stoneCost;
            BuildTime = buildTime;
            Health = health;
            Damage = damage;
            Armor = armor;
            Speed = speed;
            Range = range;
            GatheringAmount = gatheringAmount;
            GatheringSpeed = gatheringSpeed;
            GatheringCapacity = gatheringCapacity;
        }
    }
}
