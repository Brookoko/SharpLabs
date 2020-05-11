namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using DependencyInjection;
    using Xml;

    public class Queries
    {
        [Inject]
        public ProjectsHolder ProjectsHolder { get; set; }

        [Inject]
        public XmlDataLoader XmlDataLoader { get; set; }
        
        public IEnumerable<Project> AllProjects()
        {
            return ProjectsHolder.XmlProject
                .Elements("Project")
                .Select(XmlDataLoader.ToProject);
        }
        
        public Project ProjectWithId(int id)
        {
            var element = ProjectsHolder.XmlProject.Elements("Project")
                .First(p => p.Element("Id").Value == id.ToString());
            return XmlDataLoader.ToProject(element);
        }
        
        public IEnumerable<Project> CompletedProjects()
        {
            return ProjectsHolder.XmlProject
                .Elements("Project")
                .Where(p => bool.Parse(p.Element("IsCompleted").Value))
                .Select(XmlDataLoader.ToProject);
        }
        
        public IEnumerable<Project> ProjectsOrderByStart()
        {
            return ProjectsHolder.XmlProject
                .Elements("Project")
                .OrderBy(p => ToDateTime(p.Element("Start").Value))
                .Select(XmlDataLoader.ToProject);
        }
        
        public IEnumerable<Project> StartBefore(DateTime time)
        {
            return ProjectsOrderByStart()
                .Where(p => p.Start < time);
        }
        
        public IEnumerable<Project> StartAfter(DateTime time)
        {
            return ProjectsOrderByStart()
                .Where(p => p.Start > time);
        }

        public IEnumerable<Project> InRange(DateTime start, DateTime end)
        {
            return StartAfter(start)
                .Join(StartBefore(end), p => true, p => true, (p1, pp2) => p1);
        }

        public Project FirstProject()
        {
            return ProjectsOrderByStart()
                .FirstOrDefault();
        }
        
        public float CostOfProjects()
        {
            return ProjectsHolder.Projects
                .Select(p => p.Cost)
                .Sum();
        }

        public Project LastProjectOf(Worker worker)
        {
            return ProjectsHolder.Projects
                .Where(p => p.IsCompleted)
                .OrderByDescending(p => p.End)
                .FirstOrDefault(p => p.Workers.Contains(worker));
        }

        public IEnumerable<Project> CurrentlyWorkingOn(Worker worker)
        {
            return ProjectsHolder.Projects
                .Where(p => !p.IsCompleted && p.Workers.Contains(worker))
                .OrderByDescending(p => p.Start);
        }
        
        public string CommonName()
        {
            return ProjectsHolder.Projects
                .SelectMany(p => p.Workers)
                .GroupBy(w => w.FirstName)
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .First().Name;
        }

        public Worker WorkOnMostProjects()
        {
            return ProjectsHolder.Projects
                .SelectMany(p => p.Workers)
                .GroupBy(w => w)
                .Select(g => new
                {
                    Worker = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .First().Worker;
        }

        public int ProjectCount(Worker worker)
        {
            return ProjectsHolder.Projects
                .Count(p => p.Workers.Contains(worker));
        }

        public IEnumerable<Worker> AllWorkers()
        {
            return ProjectsHolder.Projects
                .SelectMany(p => p.Workers)
                .OrderBy(w => w.Id)
                .Distinct();
        }
        
        public IEnumerable<Worker> WorkersWithName(string name)
        {
            return AllWorkers()
                .Where(w => w.FirstName == name);
        }
        
        private DateTime ToDateTime(string value)
        {
            return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Utc);
        }
    }
}