using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public abstract class BaseState<TMachine, TContext> : IState<TMachine, TContext>
    {
        //protected BaseState();

        public TMachine Machine { get; set; }
        public TContext Context { get; set; }
        //
        // Summary:
        //     Amount of (active) time spent in this state since it was entered.
        public float TimeInState { get; }

        //
        // Summary:
        //     Called every frame after Archon.SwissArmyLib.Automata.BaseState`2.Reason, if
        //     the state hasn't been changed.
        public virtual void Act(float deltaTime)
        {

        }
        //
        // Summary:
        //     Called when the state is entered.
        public virtual void Begin()
        {

        }
        //
        // Summary:
        //     Called when the state is exited.
        public virtual void End()
        {

        }
        //
        // Summary:
        //     Called every frame just before Archon.SwissArmyLib.Automata.BaseState`2.Act(System.Single).
        //     Use this to check whether you should change state.
        public virtual void Reason()
        {

        }
    }
}
