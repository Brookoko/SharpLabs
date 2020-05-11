namespace Flow
{
    using System.IO;
    using DependencyInjection;
    using DependencyInjection.Commands;
    using Xml;

    public class CleanUpCommand : Command
    {
        [Inject]
        public XmlDataLoader XmlDataLoader { get; set; }
        
        public override void Execute()
        {
            CleanUp("projects");
            CleanUp("workers");
        }

        private void CleanUp(string folder)
        {
            var dir = new DirectoryInfo($"{XmlDataLoader.AssetPath}/{folder}");
            foreach (var file in dir.GetFiles())
            {
                file.Delete();
            }
        }
    }
}