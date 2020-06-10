using KnightsVsVikings.Script.MainSystem.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class UnitIdleState : FsmState<CUnit>
    {
        public UnitIdleState()
        {
           
        }

        public override void Begin()
        {
            base.Begin();

            Context.Animator.PlayAnimation("Idle");

            if (Thread.CurrentThread.Name == "WorkerThread")
                Thread.CurrentThread.Abort();
        }

        public override void End()
        {
            base.End();
        }

        public override void Reason()
        {
            base.Reason();

            if (Context.IsMoving == true)
            {
                Machine.ChangeState<UnitMoveToPositionState>();
            }

            if (Context.LastDeliveredTo != null)
                if (Context.GameObject.Transform.Position == Context.LastDeliveredTo.Transform.Position)// && Context.ResourceAmount == 0)
                    Machine.ChangeState<UnitGatheringState>();

            if (Context.LastGatheredFrom != null)
                if (Context.GameObject.Transform.Position == Context.LastGatheredFrom.Transform.Position)// && Context.ResourceAmount == 0)
                    Machine.ChangeState<UnitGatheringState>();
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
        }
    }
}
