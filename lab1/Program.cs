using System;
using Civilization;
using Civilization.Resources;
using Civilization.Worlds;

public class Program
{    
    public static void Main()
    {
        InitializeResourceFactory();
        ReadOptions(CreateMenu());
    }
    
    private static ConsoleOptions CreateMenu()
    {
        var world = new World();
        var options = new ConsoleOptions();
        options.RegisterState(new StartMenu(options));
        options.RegisterState(new NationMenu(options, world));
        options.RegisterState(new SingleNationMenu(options, world));
        options.RegisterState(new TerritoriesMenu(options, world));
        options.ChangeState(Options.Start);
        return options;
    }
    
    private static void ReadOptions(ConsoleOptions options)
    {
        while (true)
        {
            var option = Console.ReadLine();
            if (string.IsNullOrEmpty(option))
            {
                continue;
            }
            options.RunOption(option);
        }
    }
    
    private static void InitializeResourceFactory()
    {
        var factory = ResourceFactory.Instance;
        factory.Register<Gold>();
        factory.Register<Wood>();
        factory.Register<Stone>();
    }
}