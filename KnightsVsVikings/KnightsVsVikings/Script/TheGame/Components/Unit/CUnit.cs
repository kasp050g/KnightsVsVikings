using KnightsVsVikings;
using KnightsVsVikings.ExtensionMethods;
using KnightsVsVikings.Script.TheGame;
using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.Components.GatherComponents;
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
    public class CUnit : Component
    {
        private List<Passive> passives = new List<Passive>();

        private CAnimator animator;
        private CMove move;
        private CStats stats = new CStats();

        private FiniteStateMachine<CUnit> StateMachine;

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

                if(lastDeliveredTo.GetComponent<CDeliver>() == null)
                lastDeliveredTo.AddComponent<CDeliver>();
            }
        }


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
            stats = GameObject.GetComponent<CStats>();
            stats.Team = team;
            stats.Faction = faction;
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
            //int index = 0;
            //
            //switch (Faction)
            //{
            //    case EFaction.Knights:
            //        switch(UnitType)
            //        {
            //            case EUnitType.Footman:
            //                index = 1;
            //                break;
            //
            //            case EUnitType.Spearman:
            //                index = 2;
            //                break;
            //
            //            case EUnitType.Bowman:
            //                index = 3;
            //                break;
            //
            //            case EUnitType.Worker:
            //                index = 4;
            //                break;
            //        }
            //        break;
            //
            //    case EFaction.Vikings:
            //        switch (UnitType)
            //        {
            //            case EUnitType.Footman:
            //
            //                break;
            //
            //            case EUnitType.Spearman:
            //
            //                break;
            //
            //            case EUnitType.Bowman:
            //
            //                break;
            //
            //            case EUnitType.Worker:
            //
            //                break;
            //        }
            //        break;
            //}


            //ISQLiteRow myStatsRow = Singletons.TableContainerSingleton.StatsTable.Get(1);
            // new Old
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
            // new old
            //ISQLiteRow factionRow = Singletons.RepositoryContainerSingleton.UnitRepository.RepositoryTables.Get(PropertyFinder<SQLiteFactionModel>.Find(x => x.Name), Faction.ToString());
            //List<ISQLiteRow> unitRow = Singletons.RepositoryContainerSingleton.UnitTable.GetMultiple(PropertyFinder<SQLiteUnitModel>.Find(x => x.FactionId), factionRow.Id);
            //ISQLiteRow myStatsRow = null;
            //
            //foreach (SQLiteUnitModel row in unitRow)
            //    if (row.UnitTypeId == (int)unitType)
            //    {
            //        myStatsRow = Singletons.TableContainerSingleton.StatsTable.Get(row.StatsId);
            //        break;
            //    }
            //
            //List<PropertyInfo> myStatsProperties = myStatsRow.GetType().GetProperties().ToList();
            //myStatsProperties.RemoveAll(property => property.Name == "Id" || property.Name == "LocatedInTable");
            //List<PropertyInfo> statsProperties = stats.Stats.GetType().GetProperties().ToList();
            //
            //for (int i = 0; i < myStatsProperties.Count - 1; i++)
            //    statsProperties.ElementAt(i).SetValue(stats.Stats, myStatsProperties.ElementAt(i).GetValue(myStatsRow));
        }


        #region Old Astar Donno if to Delete
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

        private void SetVelocityX(float currentPos, float targetPos)
        {
            if (currentPos.ApproximatelyEqual(targetPos, 3f))
                GameObject.GetComponent<CMove>().Velocity = new Vector2(0, GameObject.GetComponent<CMove>().Velocity.Y);

            else if (targetPos < currentPos)
                GameObject.GetComponent<CMove>().Velocity = new Vector2(-1, GameObject.GetComponent<CMove>().Velocity.Y);

            else if (targetPos > currentPos)
                GameObject.GetComponent<CMove>().Velocity = new Vector2(1, GameObject.GetComponent<CMove>().Velocity.Y);
        }

        private void SetVelocityY(float currentPos, float targetPos)
        {
            if (currentPos.ApproximatelyEqual(targetPos, 3f))
                GameObject.GetComponent<CMove>().Velocity = new Vector2(GameObject.GetComponent<CMove>().Velocity.X, 0);

            else if (targetPos < currentPos)
                GameObject.GetComponent<CMove>().Velocity = new Vector2(GameObject.GetComponent<CMove>().Velocity.X, -1);

            else if (targetPos > currentPos)
                GameObject.GetComponent<CMove>().Velocity = new Vector2(GameObject.GetComponent<CMove>().Velocity.X, 1);
        }
        #endregion
    }
}
