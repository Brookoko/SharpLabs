namespace SM.States
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Transitions;

    public abstract class State : IState
    {
        public List<Transition> Transitions { get; } = new List<Transition>();
        
        public virtual void OnEnter()
        {
            foreach (var transition in Transitions)
            {
                transition.Rule.Prepare();
            }
        }
        
        public virtual void OnExit()
        {
            foreach (var transition in Transitions)
            {
                transition.Rule.Reset();
            }
        }
        
        public bool CanTransit()
        {
            return Transitions.Any(t => t.IsLegitimate);
        }

        public State PossibleTransition()
        {
            return Transitions
                .Where(t => t.IsLegitimate)
                .Select(t => t.To)
                .FirstOrDefault();
        }
    }
}