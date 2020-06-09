using KnightsVsVikings.Script.MainSystem.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
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

            Context.Animator.PlayAnimation($"{EUnitAnimationType.Idle}");
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
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
        }
    }
}
