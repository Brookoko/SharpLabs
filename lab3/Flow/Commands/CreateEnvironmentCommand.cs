namespace Flow.Commands
{
    using System;
    using DependencyInjection;
    using DependencyInjection.Commands;
    using Environment;

    public class CreateEnvironmentCommand : Command
    {
        [Inject]
        public World World { get; set; }
        
        public override void Execute()
        {
            var random = new Random();
            var count = (int) Math.Pow(2, random.Next(3, 6));
            for (var x = 0; x < count / 2; x++)
            {
                for (var y = 0; y < count / 2; y++)
                {
                    var position = new Position(x, y, (PositionType) random.Next(3));
                    World.AddPosition(position);
                }
            }
        }
    }
}