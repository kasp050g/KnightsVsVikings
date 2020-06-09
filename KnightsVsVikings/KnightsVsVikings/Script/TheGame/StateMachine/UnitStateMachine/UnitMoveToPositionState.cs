﻿using KnightsVsVikings.Script.MainSystem.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class UnitMoveToPositionState : FsmState<CUnit>
    {
        public override void Begin()
        {
            base.Begin();
            Context.Animator.PlayAnimation($"{EUnitAnimationType.Run}");
        }

        public override void End()
        {
            base.End();
        }

        public override void Reason()
        {
            base.Reason();

            if (!Context.IsMoving == true)
            {
                Machine.ChangeState<UnitIdleState>();

            }
        }
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);            
        }
    }
}
