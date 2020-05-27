using KnightsVsVikings.Script.MainSystem.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class UnitMoveToPositionState : FsmState<CUnit>
    {
        float time = 0;

        public override void Begin()
        {
            base.Begin();
            time = 0;
            Console.WriteLine("Unit Run Start");
            Context.Animator.PlayAnimation($"{EUnitAnimationType.Run}");
        }

        public override void End()
        {
            base.End();
            Console.WriteLine("Unit Run Slut");
        }
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            time += deltaTime;
        }

        public override void Reason()
        {
            base.Reason();
            if (time > 3)
            {
                Machine.ChangeState<UnitIdleState>();
            }
        }
    }
}
