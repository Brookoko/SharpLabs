namespace ConsoleUtils
{
    public interface IProgram
    {
        void Init();
        
        void ProcessInput();

        void ChangeOptions(IOptions options);

        void Exit();
    }
}