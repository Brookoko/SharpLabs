namespace SM.States
{
    using System.Collections.Generic;
    using System.Linq;
    using Transitions;

    public abstract class State : IState
    {
        public List<Transition> Transitions { get; } = new List<Transition>();
        
        public abstract void OnEnter();
        
        public abstract void OnExit();

        public abstract void Update();
        
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