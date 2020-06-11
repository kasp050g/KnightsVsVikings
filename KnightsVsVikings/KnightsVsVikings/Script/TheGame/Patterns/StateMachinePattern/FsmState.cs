using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public abstract class FsmState<T> : BaseState<FiniteStateMachine<T>,T>,IFsmState<T>,IState<FiniteStateMachine<T>,T>
    {

    }
}
