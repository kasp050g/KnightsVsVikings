using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class Passive
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Texture2D Icon { get; set; }
        public bool IsLock { get; set; }
        // Make
        public int FoodCost = 0;
        public int GoldCost = 0;
        public int TreeCost = 0;
        public int StoneCost = 0;
        public float BuildTime = 0;
        // Combat
        public int Health = 0;
        public int Damage = 0;
        public int Armor = 0;
        public int Speed = 0;
        public int Range = 0;
        public int AggroRange = 0;
        // Gathering
        public int GatheringAmount = 0;
        public float GatheringSpeed = 0;
        public int GatheringCapacity = 0;
    }
}
