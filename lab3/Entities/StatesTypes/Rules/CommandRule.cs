namespace Entities.StatesTypes.Rules
{
    using SM.Rules;

    public class CommandRule : IRule
    {
        private bool isFulfilled;
        
        public void Prepare()
        {
        }
        
        private void OnCommand()
        {
            isFulfilled = true;
        }
        
        public bool IsFulfilled()
        {
            return isFulfilled;
        }
        
        public void Reset()
        {
            isFulfilled = false;
        }
    }
}