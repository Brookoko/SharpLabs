namespace Data
{
    using System.Collections.Generic;

    public class ProjectsHolder
    {
        public List<Project> Projects { get; } = new List<Project>();
        
        public List<Worker> Workers { get; } = new List<Worker>();
        
        public void RegisterProject(Project project)
        {
            project.Id = Projects.Count;
            Projects.Add(project);
        }
    }
}