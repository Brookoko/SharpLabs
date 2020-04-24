namespace SM.Rules
{
    public interface IRule
    {
        void Prepare();
        
        bool IsFulfilled();
        
        void Reset();
    }
}