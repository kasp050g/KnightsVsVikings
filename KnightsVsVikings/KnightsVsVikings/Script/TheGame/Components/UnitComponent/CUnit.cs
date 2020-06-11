using KnightsVsVikings;
using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.Script.TheGame;
using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.Components.GatherComponent;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using KnightsVsVikings.SQLiteFramework.Framework.Global;
using KnightsVsVikings.SQLiteFramework.Interfaces;
using KnightsVsVikings.SQLiteFramework.Models.TheGame;
using KnightsVsVikings.SQLiteFramework.Patterns.CommandPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    // Kasper og Lukas
    public class CUnit : Component
    {
        private CAnimator animator;
        private CMove move;
        private CStats stats = new CStats();

        private FiniteStateMachine<CUnit> stateMachine;

        private ETeam team;
        private EUnitType unitType;
        private EFaction faction;

        private GameObject lastGatheredFrom,
                           lastDeliveredTo;

        public GameObject Target { get;  set; } = null;
        public CAnimator Animator { get => animator; set => animator = value; }
        public bool IsAlive { get; set; }
        public bool IsMoving { get; set; } = false;
        public bool IsGathering { get; set; } = false;
        public ETeam Team { get => team; set => team = value; }
        public EUnitType UnitType { get => unitType; set => unitType = value; }
        public EFaction Faction { get => faction; set => faction = value; }
        // Lucas --
        public int ResourceAmount { get; set; } = 0;
        public GameObject LastGatheredFrom
        {
            get
            {
                return lastGatheredFrom;
            }
            set
            {
                lastGatheredFrom = value;

                if (lastGatheredFrom.GetComponent<CGather>() == null)
                    lastGatheredFrom.AddComponent<CGather>();
            }
        }

        public GameObject LastDeliveredTo
        {
            get
            {
                return lastDeliveredTo;
            }
            set
            {
                lastDeliveredTo = value;

                if (lastDeliveredTo.GetComponent<CDeliver>() == null)
                    lastDeliveredTo.AddComponent<CDeliver>();
            }
        }
        // -- Lucas

        public CUnit()
        { }
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
            stats = GameObject.GetComponent<CStats>();
            stats.Team = team;
            stats.Faction = faction;
            MadeUnit();
            AssignSQLiteValues();
        }

        public override void Update()
        {
            base.Update();
            stateMachine.Update(Time.DeltaTime);
        }

        // Lucas
        public void MoveToLocation(Vector2 location)
        {
            if (Vector2.Distance(GameObject.Transform.Position, location) > 4f)
            {
                SetVelocityX(GameObject.Transform.Position.X,
                            location.X);

                SetVelocityY(GameObject.Transform.Position.Y,
                            location.Y);

                Vector2.Normalize(GameObject.GetComponent<CMove>().Velocity);
            }
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
            stateMachine = new FiniteStateMachine<CUnit>(this, new UnitIdleState());
            stateMachine.AddState(new UnitMoveToPositionState());
            stateMachine.AddState(new UnitChaseMyTargetState());
            stateMachine.AddState(new UnitDieState());
            switch (unitType)
            {
                case EUnitType.Worker:
                    stateMachine.AddState(new UnitMeleeAttackState());
                    stateMachine.AddState(new UnitGatheringState());
                    break;
                case EUnitType.Bowman:
                    stateMachine.AddState(new UnitRangeAttackState());
                    break;
                case EUnitType.Footman:
                    stateMachine.AddState(new UnitMeleeAttackState());
                    break;
                case EUnitType.Spearman:
                    stateMachine.AddState(new UnitMeleeAttackState());
                    break;
                default:
                    break;
            }
        }

        // Lucas
        private void SetVelocityX(float currentPos, float targetPos)
        {
            if (currentPos.ApproximatelyEqual(targetPos, 3f))
                GameObject.GetComponent<CMove>().Velocity = new Vector2(0, GameObject.GetComponent<CMove>().Velocity.Y);

            else if (targetPos < currentPos)
                GameObject.GetComponent<CMove>().Velocity = new Vector2(-1, GameObject.GetComponent<CMove>().Velocity.Y);

            else if (targetPos > currentPos)
                GameObject.GetComponent<CMove>().Velocity = new Vector2(1, GameObject.GetComponent<CMove>().Velocity.Y);
        }

        // Lucas
        private void SetVelocityY(float currentPos, float targetPos)
        {
            if (currentPos.ApproximatelyEqual(targetPos, 3f))
                GameObject.GetComponent<CMove>().Velocity = new Vector2(GameObject.GetComponent<CMove>().Velocity.X, 0);

            else if (targetPos < currentPos)
                GameObject.GetComponent<CMove>().Velocity = new Vector2(GameObject.GetComponent<CMove>().Velocity.X, -1);

            else if (targetPos > currentPos)
                GameObject.GetComponent<CMove>().Velocity = new Vector2(GameObject.GetComponent<CMove>().Velocity.X, 1);
        }

        // Lucas
        private void AssignSQLiteValues()
        {
            ISQLiteRow factionRow = Singletons.TableContainerSingleton.FactionTable.Get(PropertyFinder<SQLiteFactionModel>.Find(x => x.Name), Faction.ToString());
            List<ISQLiteRow> unitRow = Singletons.TableContainerSingleton.UnitTable.GetMultiple(PropertyFinder<SQLiteUnitModel>.Find(x => x.FactionId), factionRow.Id);
            ISQLiteRow myStatsRow = null;

            foreach (SQLiteUnitModel row in unitRow)
                if (row.UnitTypeId == (int)unitType)
                {
                    myStatsRow = Singletons.TableContainerSingleton.StatsTable.Get(row.StatsId);
                    break;
                }

            List<PropertyInfo> myStatsProperties = myStatsRow.GetType().GetProperties().ToList();
            myStatsProperties.RemoveAll(property => property.Name == "Id" || property.Name == "LocatedInTable");
            List<PropertyInfo> statsProperties = stats.Stats.GetType().GetProperties().ToList();

            for (int i = 0; i < myStatsProperties.Count - 1; i++)
                statsProperties.ElementAt(i).SetValue(stats.Stats, myStatsProperties.ElementAt(i).GetValue(myStatsRow));
        }
    }
}
