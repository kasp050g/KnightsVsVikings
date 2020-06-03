using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public interface IFsmState<T> : IState<FiniteStateMachine<T>, T>
    {
    }
}
