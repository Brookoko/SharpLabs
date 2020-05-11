namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
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
        
        public IEnumerable<Worker> AllWorkers()
        {
            return ProjectsHolder.XmlWorkers
                .Elements("Worker")
                .Select(XmlDataLoader.ToWorker);
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
                .Where(p => ToBool(p.Element("IsCompleted")))
                .Select(XmlDataLoader.ToProject);
        }
        
        public IEnumerable<Project> ProjectsOrderByStart()
        {
            return ProjectsHolder.XmlProject
                .Elements("Project")
                .OrderBy(p => ToDateTime(p.Element("Start")))
                .Select(XmlDataLoader.ToProject);
        }
        
        public IEnumerable<Project> StartBefore(DateTime time)
        {
            return ProjectsHolder.XmlProject
                .Elements("Project")
                .Where(p => ToDateTime(p.Element("Start")) < time)
                .Select(XmlDataLoader.ToProject);
        }
        
        public IEnumerable<Project> StartAfter(DateTime time)
        {
            return ProjectsHolder.XmlProject
                .Elements("Project")
                .Where(p => ToDateTime(p.Element("Start")) > time)
                .Select(XmlDataLoader.ToProject);
        }

        public IEnumerable<Project> InRange(DateTime start, DateTime end)
        {
            return StartAfter(start)
                .Join(StartBefore(end), p => true, p => true, (p1, pp2) => p1);
        }
        
        public float CostOfProjects()
        {
            return ProjectsHolder.XmlProject
                .Elements("Project")
                .Select(p => p.Element("Cost"))
                .Select(cost => float.Parse(cost.Value))
                .Sum();
        }

        public Project LastProjectOf(int id)
        {
            var element = ProjectsHolder.XmlProject
                .Elements("Project")
                .Where(p => ToBool(p.Element("IsCompleted")))
                .OrderByDescending(p => ToDateTime(p.Element("End")))
                .FirstOrDefault();
            return XmlDataLoader.ToProject(element);
        }

        public IEnumerable<Project> CurrentlyWorkingOn(int id)
        {
            return ProjectsHolder.XmlProject
                .Elements("Project")
                .Where(p => p.Elements("Workers").Select(el => el.Value).Contains(id.ToString()))
                .Select(XmlDataLoader.ToProject);
        }
        
        public Worker WorkOnMostProjects()
        {
            var common = ProjectsHolder.XmlProject
                .Elements("Project")
                .SelectMany(p => p.Elements("Workers"))
                .Select(w => int.Parse(w.Value))
                .GroupBy(id => id)
                .Select(g => new
                {
                    Id = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .First().Id;
            return XmlDataLoader.ToWorker(common);
        }

        public int ProjectCount(int id)
        {
            return ProjectsHolder.XmlProject
                .Elements("Project")
                .SelectMany(p => p.Elements("Workers"))
                .Select(w => int.Parse(w.Value))
                .Count(w => w == id);
        }
        
        public IEnumerable<Worker> WorkersWithName(string name)
        {
            return ProjectsHolder.XmlWorkers
                .Elements("Worker")
                .Where(w => w.Element("FirstName").Value == name)
                .Select(XmlDataLoader.ToWorker);
        }
        
        private bool ToBool(XElement element)
        {
            return XmlConvert.ToBoolean(element.Value);
        }
        
        private DateTime ToDateTime(XElement element)
        {
            return XmlConvert.ToDateTime(element.Value, XmlDateTimeSerializationMode.Utc);
        }
    }
}