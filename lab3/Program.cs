namespace lab3
{
    using AppContext;
    using ConsoleApp;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var context = new AppContext();
            context.Start();
            var program = new ConsoleProgram();
            program.Init();
            program.ProcessInput();
            program.Exit();
        }
    }
}