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

        public override void Begin()
        {
            base.Begin();

            workerThread = new Thread(WorkerThread);
            workerThread.IsBackground = true;
            workerThread.Start();
        }

        public override void End()
        {
            base.End();
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

        }
    }
}
