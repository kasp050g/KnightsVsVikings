using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class FiniteStateMachine<T>
    {
        //
        // Summary:
        //     A shared context which all states have access to.
        public T Context { get; }
        //
        // Summary:
        //     The active state.
        public IFsmState<T> CurrentState { get; private set; }
        //
        // Summary:
        //     The previously active state.
        public IFsmState<T> PreviousState { get; private set; }
        private Dictionary<Type, IFsmState<T>> stateDictionary = new Dictionary<Type, IFsmState<T>>();

        //
        // Summary:
        //     Creates a new Finite State Machine. If you need control over how the states are
        //     created, you can register them manually using Archon.SwissArmyLib.Automata.FiniteStateMachine`1.RegisterState(Archon.SwissArmyLib.Automata.IFsmState{`0}).
        //     If not, then you can freely use Archon.SwissArmyLib.Automata.FiniteStateMachine`1.ChangeStateAuto``1
        //     which will create the states using their default constructor.
        //
        // Parameters:
        //   context:
        //     A shared context for the states.
        public FiniteStateMachine(T context)
        {
            this.Context = context;
        }
        //
        // Summary:
        //     Creates a new Finite State Machine and changes the state to startState. If you
        //     need control over how the states are created, you can register them manually
        //     using Archon.SwissArmyLib.Automata.FiniteStateMachine`1.RegisterState(Archon.SwissArmyLib.Automata.IFsmState{`0}).
        //     If not, then you can freely use Archon.SwissArmyLib.Automata.FiniteStateMachine`1.ChangeStateAuto``1
        //     which will create the states using their default constructor.
        //
        // Parameters:
        //   context:
        //
        //   startState:
        public FiniteStateMachine(T context, IFsmState<T> startState)
        {
            this.Context = context;
            this.CurrentState = startState;

            startState.Machine = this;
            startState.Context = context;
            this.CurrentState.Begin();
            stateDictionary.Add(startState.GetType(), startState);
        }



        //
        // Summary:
        //     Changes the active state to the given state type. An instance of that type should
        //     already had been registered to use this method.
        //
        // Type parameters:
        //   TState:
        public void ChangeState<TState>() where TState : IFsmState<T>
        {
            PreviousState = CurrentState;

            CurrentState = stateDictionary[typeof(TState)];

            PreviousState.End();
            CurrentState.Begin();

            //return this.;
        }
        //
        // Summary:
        //     Changes the active state to a specific state instance. This will (if not null)
        //     also register the state.
        //
        // Parameters:
        //   state:
        //
        // Type parameters:
        //   TState:
        public TState ChangeState<TState>(TState state) where TState : IFsmState<T>
        {
            return state;
        }
        //
        // Summary:
        //     Changes the active state to the given state type. If a state of that type isn't
        //     already registered, it will automatically create a new instance using the empty
        //     constructor.
        //
        // Type parameters:
        //   TState:
        //public TState ChangeStateAuto<TState>() where TState : IFsmState<T>, new()
        //{

        //}
        //
        // Summary:
        //     Checks whether a state type is registered.
        //
        // Parameters:
        //   stateType:
        //     The state type to check.
        //
        // Returns:
        //     True if registered, false otherwise.
        //public bool IsStateRegistered(Type stateType)
        //{

        //}
        //
        // Summary:
        //     Generic version of Archon.SwissArmyLib.Automata.FiniteStateMachine`1.IsStateRegistered(System.Type).
        //     Checks whether a state type is registered.
        //
        // Type parameters:
        //   TState:
        //     The state type to check.
        //
        // Returns:
        //     Tru if registered, false otherwise.
        //public bool IsStateRegistered<TState>() where TState : IFsmState<T>
        //{

        //}
        //
        // Summary:
        //     Preemptively add a state instance. Useful if the state doesn't have an empty
        //     constructor and therefore cannot be used with ChangeStateAuto.
        //
        // Parameters:
        //   state:
        //     The state to register.
        public void RegisterState(IFsmState<T> state)
        {
            state.Machine = this;
            state.Context = Context;
            stateDictionary.Add(state.GetType(),state);
        }


        //
        // Summary:
        //     Call this every time the machine should update. Eg. every frame.
        public void Update(float deltaTime)
        {
            var currentState = CurrentState;

            if (currentState != null)
            {
                currentState.Reason();

                // we only want to update the state if it's still the current one
                if (currentState == CurrentState)
                    CurrentState.Act(deltaTime);
            }
        }
    }
}
