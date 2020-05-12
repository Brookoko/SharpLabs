namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DependencyInjection;

    public class Queries
    {
        [Inject]
        public ProjectsHolder ProjectsHolder { get; set; }
        
        public IEnumerable<Project> AllProjects()
        {
            return ProjectsHolder.Projects;
        }
        
        public IEnumerable<Project> CompletedProjects()
        {
            return ProjectsHolder.Projects
                .Where(p => p.IsCompleted);
        }
        
        public IEnumerable<Project> ProjectsOrderByStart()
        {
            return ProjectsHolder.Projects
                .OrderBy(p => p.Start);
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
                .Join(StartBefore(end), p => p.Id, p => p.Id, (p1, pp2) => p1);
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
    }
}