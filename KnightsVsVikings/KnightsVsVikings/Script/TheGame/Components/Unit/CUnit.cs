using KnightsVsVikings;
using KnightsVsVikings.Script.TheGame;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class CUnit : Component
    {
        private Stats stats = new Stats();
        private List<Passive> passives = new List<Passive>();

        private CAnimator animator;
        private CMove move;

        private FiniteStateMachine<CUnit> StateMachine;

        private ETeam team;
        private EUnitType unitType;
        private EFaction faction;

        public GameObject Target { get; protected set; } = null;
        public CAnimator Animator { get => animator; set => animator = value; }
        public bool IsAlive { get; set; }

        public ETeam Team { get => team; set => team = value; }
        public EUnitType UnitType { get => unitType; set => unitType = value; }
        public EFaction Faction { get => faction; set => faction = value; }

        public CUnit()
        {

        }
        public CUnit(ETeam team, EUnitType unitType, EFaction faction)
        {
            this.team = team;
            this.unitType = unitType;
            this.faction = faction;
        }
        public override void Awake()
        {
            base.Awake();
            animator = GameObject.GetComponent<CAnimator>();
            move = GameObject.GetComponent<CMove>();

            MadeUnit();
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

            if (Input.GetKey(Keys.Up))
            {
                newVel += new Vector2(0, -1);
            }
            if (Input.GetKey(Keys.Down))
            {
                newVel += new Vector2(0, 1);
            }
            if (Input.GetKey(Keys.Left))
            {
                newVel += new Vector2(-1, 0);
            }
            if (Input.GetKey(Keys.Right))
            {
                newVel += new Vector2(1, 0);
            }

            move.Velocity = newVel;
        }

        public void MoveToLocation(Vector2 location)
        {
            if (Vector2.Distance(GameObject.Transform.Position, location) > 4f)
            {
                SetVelocityX(GameObject.Transform.Position.X,
                            location.X);

                SetVelocityY(GameObject.Transform.Position.Y,
                            location.Y);

                Vector2.Normalize(GameObject.Transform.Velocity);
            }
        }

        private void SetVelocityX(float currentPos, float targetPos)
        {
            if (currentPos.ApproximatelyEqual(targetPos, 3f))
                GameObject.Transform.Velocity = new Vector2(0, GameObject.Transform.Velocity.Y);

            else if (targetPos < currentPos)
                GameObject.Transform.Velocity = new Vector2(-1, GameObject.Transform.Velocity.Y);

            else if (targetPos > currentPos)
                GameObject.Transform.Velocity = new Vector2(1, GameObject.Transform.Velocity.Y);
        }

        private void SetVelocityY(float currentPos, float targetPos)
        {
            if (currentPos.ApproximatelyEqual(targetPos, 3f))
                GameObject.Transform.Velocity = new Vector2(GameObject.Transform.Velocity.X, 0);

            else if (targetPos < currentPos)
                GameObject.Transform.Velocity = new Vector2(GameObject.Transform.Velocity.X, - 1);

            else if (targetPos > currentPos)
                GameObject.Transform.Velocity = new Vector2(GameObject.Transform.Velocity.X, 1);
        }

        private void MadeUnit()
        {        
            // Add Animations
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(faction, unitType, EUnitAnimationType.Idle));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(faction, unitType, EUnitAnimationType.Run));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(faction, unitType, EUnitAnimationType.BowAttack));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(faction, unitType, EUnitAnimationType.SpearAttack));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(faction, unitType, EUnitAnimationType.SwordAttack));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(faction, unitType, EUnitAnimationType.Die));
            animator.AddAnimation(BlendTreeContainer.Instance.GetUnitBlendTree(faction, unitType, EUnitAnimationType.Cast));
            animator.PlayAnimation($"{EUnitAnimationType.Idle}");

            // StateMachine
            StateMachine = new FiniteStateMachine<CUnit>(this, new UnitIdleState());
            StateMachine.AddState(new UnitMoveToPositionState());
            StateMachine.AddState(new UnitChaseMyTargetState());
            StateMachine.AddState(new UnitDieState());
            switch (unitType)
            {
                case EUnitType.Worker:
                    StateMachine.AddState(new UnitMeleeAttackState());
                    StateMachine.AddState(new UnitGatheringState());
                    break;
                case EUnitType.Bowman:
                    StateMachine.AddState(new UnitRangeAttackState());
                    break;
                case EUnitType.Footman:
                    StateMachine.AddState(new UnitMeleeAttackState());
                    break;
                case EUnitType.Spearman:
                    StateMachine.AddState(new UnitMeleeAttackState());
                    break;
                default:
                    break;
            }

            //TODO: Get stats from sql here.
        }
    }
}
