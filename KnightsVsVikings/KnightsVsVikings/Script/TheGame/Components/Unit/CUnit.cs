using KnightsVsVikings;
using MainSystemFramework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public abstract class CUnit : Component
    {
        protected Stats stats = new Stats();
        protected List<Passive> passives = new List<Passive>();
        protected GameObject myTarget = null;
        protected ETeam eTeam;
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

        protected void UnitIdleBehaviour()
        {

        }
        protected void UnitMovementBehaviour()
        {

        }
        protected void UnitCombatBehaviour()
        {

        }
        protected void UnitDealDamage()
        {

        }
        public void UnitTakeDamage(int damage)
        {
            damage -= stats.Armor;
            if (damage <= 0)
            {
                stats.Health -= 1;
            }
            else
            {
                stats.Health -= damage;
            }

            if (stats.Health <= 0)
            {
                UnitDie();
            }
        }

        protected void UnitDie()
        {

        }
    }
}
