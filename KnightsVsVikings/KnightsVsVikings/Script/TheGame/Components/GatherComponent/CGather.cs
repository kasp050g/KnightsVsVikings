using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.GatherComponent
{
    //Lucas
    class CGather : Component
    {
        private Semaphore resourceCapacity = new Semaphore(4, 4);

        public void GatherResource(CUnit worker)
        {
            resourceCapacity.WaitOne();

            worker.Animator.PlayAnimation($"{EUnitAnimationType.Cast}");

            Thread.Sleep(1500);

            worker.ResourceAmount = 100;
            worker.LastGatheredFrom = GameObject;

            resourceCapacity.Release();
        }
    }
}
