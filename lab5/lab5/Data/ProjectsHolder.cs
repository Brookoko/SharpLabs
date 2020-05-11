namespace Data
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class ProjectsHolder
    {
        public XElement XmlProject { get; } = new XElement("Projects");

        public XElement XmlWorkers { get; } = new XElement("Workers");
        
        public List<Project> Projects { get; } = new List<Project>();
        
        public List<Worker> Workers { get; } = new List<Worker>();
        
        public void RegisterProject(Project project)
        {
            project.Id = Projects.Count;
            Projects.Add(project);
        }
    }
}