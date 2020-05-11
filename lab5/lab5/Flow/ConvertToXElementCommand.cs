namespace Flow
{
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Data;
    using DependencyInjection;
    using DependencyInjection.Commands;
    using Xml;

    public class ConvertToXElementCommand : Command
    {
        [Inject]
        public ProjectsHolder ProjectsHolder { get; set; }

        [Inject]
        public XmlDataLoader XmlDataLoader { get; set; }
        
        public override void Execute()
        {
            foreach (var worker in ProjectsHolder.Workers)
            {
                XmlDataLoader.ToXml(worker);
            }
            foreach (var project in ProjectsHolder.Projects)
            {
                XmlDataLoader.ToXml(project);
            }
            for (var i = 0; i < ProjectsHolder.Workers.Count; i++)
            {
                var worker = XElement.Load($"{XmlDataLoader.AssetPath}/workers/{i}.xml");
                ProjectsHolder.XmlWorkers.Add(worker);
            }
            for (var i = 0; i < ProjectsHolder.Projects.Count; i++)
            {
                var project = XElement.Load($"{XmlDataLoader.AssetPath}/projects/{i}.xml");
                ProjectsHolder.XmlProject.Add(project);
            }
        }
    }
}