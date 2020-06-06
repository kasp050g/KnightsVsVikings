using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class Stats
    {
        // Make
        public int FoodCost { get; set; } = 0;
        public int GoldCost { get; set; } = 0;
        public int WoodCost { get; set; } = 0;
        public int StoneCost { get; set; } = 0;        
        public float BuildTime { get; set; } = 0;
        // Combat
        public int Health { get; set; } = 0;
        public int Damage { get; set; } = 0;
        public int Armor { get; set; } = 0;
        public float Speed { get; set; } = 0;
        public int Range { get; set; } = 0;
        // Gathering
        public int GatheringAmount { get; set; } = 0;
        public float GatheringSpeed { get; set; } = 0;
        public int GatheringCapacity { get; set; } = 0;
    }
}
