namespace SM
{
    using States;
    
    public class StateMachine
    {
        private State current;
        
        public void Init(State state)
        {
            current = state;
            current.OnEnter();
        }
        
        public void Update()
        {
            TryToTransit();
        }
        
        private void TryToTransit()
        {
            if (current.CanTransit())
            {
                var next = current.PossibleTransition();
                current.OnExit();
                next.OnEnter();
                current = next;
            }
        }
    }
}