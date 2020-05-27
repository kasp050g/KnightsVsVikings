using KnightsVsVikings;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        protected CAnimator animator;
        protected CMove move;
        protected FiniteStateMachine<CUnit> StateMachine;

        public CAnimator Animator { get => animator; set => animator = value; }

        public override void Awake()
        {
            base.Awake();
            animator = GameObject.GetComponent<CAnimator>();
            move = GameObject.GetComponent<CMove>();
            StateMachine = new FiniteStateMachine<CUnit>(this, new UnitIdle());
            StateMachine.RegisterState(new UnitRun());
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
            StateMachine.Update(Time.deltaTime);
            NewAnimation();
            Test();
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

        public void NewAnimation()
        {
            if (Input.GetKeyDown(Keys.D1) && !animator.AnimationLock)
            {
                animator.PlayAnimation(EUnitAnimationType.Idle.ToString());
            }
            if (Input.GetKeyDown(Keys.D2) && !animator.AnimationLock)
            {
                animator.PlayAnimation($"{EUnitAnimationType.Run}");
            }
            if (Input.GetKeyDown(Keys.D3) && !animator.AnimationLock)
            {
                animator.PlayAnimation($"{EUnitAnimationType.BowAttack}");
            }
            if (Input.GetKeyDown(Keys.D4) && !animator.AnimationLock)
            {
                animator.PlayAnimation($"{EUnitAnimationType.SpearAttack}");
            }
            if (Input.GetKeyDown(Keys.D5) && !animator.AnimationLock)
            {
                animator.PlayAnimation($"{EUnitAnimationType.SwordAttack}");
            }
            if (Input.GetKeyDown(Keys.D6) && !animator.AnimationLock)
            {
                animator.PlayAnimation($"{EUnitAnimationType.Die}");
            }
            if (Input.GetKeyDown(Keys.D7) && !animator.AnimationLock)
            {
                animator.PlayAnimation($"{EUnitAnimationType.Cast}");
            }
            if (Input.GetKeyDown(Keys.D8))
            {
                animator.Reset();
            }
        }

        public void Test()
        {
            Vector2 newVel = new Vector2(0, 0);

            if (Input.GetKey(Keys.W))
            {
                newVel += new Vector2(0, -1);
            }
            if (Input.GetKey(Keys.S))
            {
                newVel += new Vector2(0, 1);
            }
            if (Input.GetKey(Keys.A))
            {
                newVel += new Vector2(-1, 0);
            }
            if (Input.GetKey(Keys.D))
            {
                newVel += new Vector2(1, 0);
            }

            move.Velocity = newVel;
        }
    }
}
