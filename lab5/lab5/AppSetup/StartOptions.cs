namespace AppSetup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ConsoleApp;
    using Data;
    using DependencyInjection;
    using lab5.Xml;

    public class StartOptions : Options
    {
        [Inject]
        public Queries Queries { get; set; }

        [Inject]
        public XmlDataLoader XmlDataLoader { get; set; }
        
        public override string Id => "Start";
        
        public StartOptions()
        {
            AddOption("--toXml", _ => ToXml());
            AddOption("--fromXml #id", p => FromXml(p.Int));
            AddOption("--all", _ => PrintResult(Queries.AllProjects()));
            AddOption("--completed", _ => PrintResult(Queries.CompletedProjects()));
            AddOption("--order", _ => PrintResult(Queries.ProjectsOrderByStart()));
            AddOption("--before #y #m #d", p => PrintResult(Queries.StartBefore(ToDate(p.Ints.ToArray()))));
            AddOption("--after #y #m #d", p => PrintResult(Queries.StartAfter(ToDate(p.Ints.ToArray()))));
            AddOption("--range #y1 #m1 #d1 #y2 #m2 #d2", p => PrintResult(Range(p.Ints)));
            AddOption("--first", p => PrintResult(Queries.FirstProject()));
            AddOption("--cost", p => PrintResult(Queries.CostOfProjects()));
            AddOption("--lastOf #id", p =>  PrintResult(Queries.LastProjectOf(GetWorker(p.Int))));
            AddOption("--workingOn #id", p =>  PrintResult(Queries.CurrentlyWorkingOn(GetWorker(p.Int))));
            AddOption("--withName $name", p => PrintResult(Queries.WorkersWithName(p.String)));
            AddOption("--common", _ => PrintResult(Queries.CommonName()));
            AddOption("--most", _ => PrintResult(Queries.WorkOnMostProjects()));
            AddOption("--count #id", p =>  PrintResult(Queries.ProjectCount(GetWorker(p.Int))));
            AddOption("--allWorkers", _ => PrintResult(Queries.AllWorkers()));
        }
        
        private void FromXml(int id)
        {
            var project = XmlDataLoader.FromXml(id);
            PrintResult(project);
        }
        
        private void ToXml()
        {
            foreach (var worker in Queries.AllWorkers())
            {
                XmlDataLoader.ToXml(worker);
            }
            foreach (var project in Queries.AllProjects())
            {
                XmlDataLoader.ToXml(project);
            }
        }
        
        private Project GetProject(int id)
        {
            return Queries.AllProjects().First(p => p.Id == id);
        }

        private Worker GetWorker(int id)
        {
            return Queries.AllWorkers().First(w => w.Id == id);
        }
        
        private IEnumerable<Project> Range(List<int> ints)
        {
            var start = ToDate(ints.Take(3).ToArray());
            var end = ToDate(ints.Skip(3).ToArray());
            return Queries.InRange(start, end);
        }
        
        private DateTime ToDate(int[] ints)
        {
            return new DateTime(ints[0], ints[1], ints[2]);
        }
        
        private void PrintResult<T>(IEnumerable<T> result)
        {
            foreach (var res in result)
            {
                Console.WriteLine(res);
            }
        }

        private void PrintResult<T>(T res)
        {
            Console.WriteLine(res);
        }
    }
}