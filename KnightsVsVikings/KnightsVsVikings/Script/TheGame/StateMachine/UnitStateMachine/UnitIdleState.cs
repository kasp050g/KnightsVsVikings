using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class UnitIdleState : FsmState<CUnit>
    {
        float time = 0;
        public UnitIdleState()
        {
           
        }

        public override void Begin()
        {
            base.Begin();
            time = 0;
            Console.WriteLine("Unit Ilde Start");
            Context.Animator.PlayAnimation($"{EUnitAnimationType.Idle}");
        }

        public override void End()
        {
            base.End();
            Console.WriteLine("Unit Idle slut");
        }

        public override void Reason()
        {
            base.Reason();
            if (time > 3)
            {
                Machine.ChangeState<UnitMoveToPositionState>();
            }
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            time += deltaTime;
        }
    }
}
