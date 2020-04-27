namespace SM
{
    using States;
    
    public class StateMachine
    {
        private State current;
        private bool shouldProcess;
        
        public void Init(State state)
        {
            current = state;
        }

        public void Start()
        {
            shouldProcess = true;
            current.OnEnter();
        }
        
        public void Update()
        {
            if (shouldProcess)
            {
                TryToTransit();
            }
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

        public void Stop()
        {
            shouldProcess = false;
            current.OnExit();
            current = null;
        }
    }
}