using MainSystemFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsVsVikings.Script.TheGame.Components.GatherComponents
{
    class CDeliver : Component
    {
        private Semaphore garrisonCapacity = new Semaphore(6, 6);

        public void DeliverResource(CUnit worker)
        {
            garrisonCapacity.WaitOne();

            Thread.Sleep(1000);

            Console.WriteLine($"Delivered: {worker.ResourceAmount}");

            worker.ResourceAmount = 0;
            worker.LastDeliveredTo = GameObject;

            garrisonCapacity.Release();
        }
    }
}
