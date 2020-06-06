using MainSystemFramework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class CStats : Component
    {
        private ETeam team;
        private EFaction faction;
        private Stats stats = new Stats();

        public ETeam Team { get => team; set => team = value; }
        public EFaction Faction { get => faction; set => faction = value; }
        public Stats Stats { get => stats; set => stats = value; }

        //Stats baseStats;
        //Stats currentStats;

        //List<Passive> passives = new List<Passive>();
        public CStats()
        {

        }
        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            base.Update();
        }

        public void UpdatePassives()
        {

        }
    }
}
