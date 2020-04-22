using Administration;

public class Program
{    
    public static void Main()
    {
        var program = new AdministrationProgram();
        program.Init();
        program.ProcessInput();
        program.Exit();
    }
}
