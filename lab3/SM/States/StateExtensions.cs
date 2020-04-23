namespace SM.States
{
    using System.Linq;
    using Rules;
    using Transitions;

    public static class StateExtensions
    {
        public static StateTransitionBuilder To(this State from, State to)
        {
            return new StateTransitionBuilder(from, to);
        }
    }

    public class StateTransitionBuilder
    {
        private readonly State from;
        private readonly State to;
        
        public StateTransitionBuilder(State from, State to)
        {
            this.from = from;
            this.to = to;
            CreateTransition();
        }
        
        private void CreateTransition()
        {
            var transition = from.Transitions.FirstOrDefault(t => t.To == to);
            if (transition != null) return;
            transition = new Transition {To = to};
            from.Transitions.Add(transition);
        }
        
        public StateTransitionBuilder If(IRule rule)
        {
            var transition = from.Transitions.First(t => t.To == to);
            transition.Rule = rule;
            return this;
        }
    }
}