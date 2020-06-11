using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    public class FiniteStateMachine<T>
    {
        /// <summary>
        /// A shared context which all states have access to.
        /// </summary>
        public T Context { get; private set; }
        /// <summary>
        /// The active state.
        /// </summary>        
        public IFsmState<T> CurrentState { get; private set; }
        /// <summary>
        /// The previously active state.
        /// </summary>
        public IFsmState<T> PreviousState { get; private set; }
        /// <summary>
        /// All the state's
        /// </summary>
        private Dictionary<Type, IFsmState<T>> stateDictionary = new Dictionary<Type, IFsmState<T>>();

        /// <summary>
        /// Creates a new Finite State Machine and changes the state to startState.
        /// </summary>
        /// <param name="context">Type</param>
        /// <param name="startState">Start State</param>
        public FiniteStateMachine(T context, IFsmState<T> startState)
        {
            this.Context = context;
            this.CurrentState = startState;

            startState.Machine = this;
            startState.Context = context;
            this.CurrentState.Begin();
            stateDictionary.Add(startState.GetType(), startState);
        }

        /// <summary>
        /// hanges the active state to the given state type. An instance of that type should
        /// already had been registered to use this method.
        /// </summary>
        /// <typeparam name="TState">The State you want to change to</typeparam>
        /// <returns></returns>
        public TState ChangeState<TState>() where TState : IFsmState<T>
        {
            PreviousState = CurrentState;

            CurrentState = stateDictionary[typeof(TState)];

            PreviousState.End();
            CurrentState.Begin();

            return (TState)CurrentState;
        }

        /// <summary>
        /// Changes the active state to a specific state instance. This will (if not null)
        /// also register the state.
        /// </summary>
        /// <typeparam name="TState">The State you want to change to</typeparam>
        /// <param name="state">The State you want to change to and it value</param>
        /// <returns></returns>
        public TState ChangeState<TState>(TState state) where TState : IFsmState<T>
        {
            if (CurrentState != null)
                CurrentState.End();

            PreviousState = CurrentState;
            CurrentState = state;

            if (CurrentState != null)
            {
                AddState(state);
                CurrentState.Machine = this;
                CurrentState.Context = Context;
                CurrentState.Begin();
            }

            return state;
        }

        /// <summary>
        /// Preemptively add a state instance. Useful if the state doesn't have an empty
        /// constructor and therefore cannot be used with ChangeStateAuto.
        /// </summary>
        /// <param name="state">The state to register.</param>  
        public void AddState(IFsmState<T> state)
        {
            state.Machine = this;
            state.Context = Context;
            stateDictionary.Add(state.GetType(),state);
        }

        /// <summary>
        /// Call this every time the machine should update. Eg. every frame.
        /// </summary>
        /// <param name="deltaTime"></param>
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
