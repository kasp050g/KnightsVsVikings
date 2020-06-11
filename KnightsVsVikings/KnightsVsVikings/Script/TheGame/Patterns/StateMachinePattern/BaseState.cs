using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    /// <summary>
    /// A simple abstract class that implements <see cref="IState{T, T}"/>.
    /// 
    /// You might be looking for <see cref="FsmState{T}"/> or <see cref="PdaState{T}"/>.
    /// </summary>
    /// <typeparam name="TMachine">The type of the machine.</typeparam>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public abstract class BaseState<TMachine, TContext> : IState<TMachine, TContext>
    {
        /// <inheritdoc />
        public TMachine Machine { get; set; }

        /// <inheritdoc />
        public TContext Context { get; set; }

        /// <summary>
        /// Amount of (active) time spent in this state since it was entered.
        /// </summary>
        public float TimeInState { get; }

        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        public virtual void Begin()
        {

        }

        /// <summary>
        /// Called when the state is exited.
        /// </summary>
        public virtual void End()
        {

        }

        /// <summary>
        /// Called every frame after Reason, if
        /// the state hasn't been changed.
        /// </summary>        
        public virtual void Reason()
        {

        }

        /// <summary>
        /// Called every frame just before Act.
        /// Use this to check whether you should change state.
        /// </summary>
        /// <param name="deltaTime">gametime</param>
        public virtual void Act(float deltaTime)
        {

        }
    }
}
