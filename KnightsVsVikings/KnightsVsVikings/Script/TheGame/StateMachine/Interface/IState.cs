using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public interface IState<TMachine, TContext>
    {
        //
        // Summary:
        //     The state machine this state belongs to.
        TMachine Machine { get; set; }
        //
        // Summary:
        //     The context for this state.
        TContext Context { get; set; }

        //
        // Summary:
        //     Called every frame after Archon.SwissArmyLib.Automata.IState`2.Reason, if the
        //     state hasn't been changed.
        void Act(float deltaTime);
        //
        // Summary:
        //     Called when the state is entered.
        void Begin();
        //
        // Summary:
        //     Called when the state is exited.
        void End();
        //
        // Summary:
        //     Called every frame just before Archon.SwissArmyLib.Automata.IState`2.Act(System.Single).
        //     Use this to check whether you should change state.
        void Reason();
    }
}
