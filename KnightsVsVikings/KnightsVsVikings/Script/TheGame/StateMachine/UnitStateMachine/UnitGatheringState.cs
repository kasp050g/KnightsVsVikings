using KnightsVsVikings.Script.TheGame.Components.AstarComponent;
using KnightsVsVikings.Script.TheGame.Components.GatherComponents;
using KnightsVsVikings.Script.TheGame.Patterns.SingletonPattern;
using MainSystemFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class UnitGatheringState : FsmState<CUnit>
    {
        Thread workerThread;

        private bool workerIsActive = true,
                     workerEnd = false;

        public override void Begin()
        {
            base.Begin();

            workerIsActive = true;

            workerThread = new Thread(WorkerThread);
            workerThread.Name = "WorkerThread";
            workerThread.IsBackground = true;
            workerThread.Start();
            
        }

        public override void End()
        {
            base.End();

            if (Context.IsMoving == true)
            {
                //workerThread.Abort();
                Machine.ChangeState<UnitMoveToPositionState>();
            }

            if (workerEnd)
            {
                //workerThread.Abort();
                Machine.ChangeState<UnitIdleState>();
            }
        }

        public override void Reason()
        {
            base.Reason();
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
        }

        private void WorkerThread()
        {
            while (workerIsActive)
            {
                Thread.Sleep(10);

                if (Context.LastDeliveredTo == null)
                    Context.LastDeliveredTo = GetNearestHall();

                if (Context.LastGatheredFrom == null)
                    Context.LastGatheredFrom = GetNearestResource();

                AutoGatherBehaviour();
            }

            if (Context.IsMoving == true)
            {
                Machine.ChangeState<UnitMoveToPositionState>();
            }

            //workerThread.Abort();
        }

        private void AutoGatherBehaviour()
        {
            if (Context.GameObject.Transform.Position == Context.LastDeliveredTo.Transform.Position)
            {
                Context.LastDeliveredTo.GetComponent<CDeliver>().DeliverResource(Context);
                Context.GameObject.GetComponent<CAstar>().ResetAstar();
                Context.GameObject.GetComponent<CUnit>().Target = Context.LastGatheredFrom;
                Context.GameObject.GetComponent<CUnit>().IsMoving = true;
                workerIsActive = false;
                //workerEnd = true;
            }

            if (Context.GameObject.Transform.Position == Context.LastGatheredFrom.Transform.Position)
            {
                Context.LastGatheredFrom.GetComponent<CGather>().GatherResource(Context);
                Context.GameObject.GetComponent<CAstar>().ResetAstar();
                Context.GameObject.GetComponent<CUnit>().Target = Context.LastDeliveredTo;
                Context.GameObject.GetComponent<CUnit>().IsMoving = true;
                workerIsActive = false;
                //workerEnd = true;
            }
        }

        private GameObject GetNearestHall()
        {
            try
            {
                return Singletons.LevelInformationSingleton.GetBuildingTiles()
                    .Where(building => building.GetComponent<CBuilding>().BuildingType == EBuildingType.TownHall && building.GetComponent<CBuilding>().Faction == Context.Faction)
                    .Select(building => new GameObject() { Transform = building.Transform })
                    .OrderBy(building => Vector2.Distance(building.Transform.Position, Context.GameObject.Transform.Position))
                    .First();
            }
            catch
            {
                return null;
            }
        }

        private GameObject GetNearestResource()
        {
            try
            {
                return Singletons.LevelInformationSingleton.GetResourceTiles()
                    .Select(resource => new GameObject() { Transform = resource.Transform })
                    .OrderBy(resource => Vector2.Distance(resource.Transform.Position, Context.GameObject.Transform.Position))
                    .First();
            }
            catch
            {
                return null;
            }
        }
    }
}
