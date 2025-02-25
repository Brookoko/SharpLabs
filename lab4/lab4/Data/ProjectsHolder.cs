namespace Data
{
    using System.Collections.Generic;

    public class ProjectsHolder
    {
        public IEnumerable<Project> Projects => projects;
        
        private readonly List<Project> projects = new List<Project>();
        
        public void RegisterProject(Project project)
        {
            project.Id = projects.Count;
            projects.Add(project);
        }
    }
}