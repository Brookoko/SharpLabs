namespace SM.States
{
    public interface IState
    {
        void OnEnter();
        
        void OnExit();
    }
}