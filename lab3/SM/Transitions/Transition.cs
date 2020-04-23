namespace SM.Transitions
{
    using Rules;
    using States;

    public class Transition
    {
        public State To { get; set; }
        
        public IRule Rule { get; set; }
        
        public bool IsLegitimate => Rule.IsFulfilled();
    }
}