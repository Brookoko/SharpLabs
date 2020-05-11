namespace Flow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using DependencyInjection;
    using DependencyInjection.Commands;
    
    public class SetupCommand : Command
    {
        [Inject]
        public ProjectsHolder ProjectsHolder { get; set; }
        
        private readonly List<string> projectNames = new List<string>
        {
            "Make America great again",
            "Develop the Skynet",
            "Take a nap",
            "Make a cup of coffee",
            "Protect the wall",
            "Show the way",
            "Just make something"
        };
        
        private readonly List<string> workersNames = new List<string>
        {
            "John",
            "Alan",
            "Robert",
            "Harry",
            "Gabe",
            "Nikita"
        };
        
        private readonly List<string> secondNames = new List<string>
        {
            "Smith",
            "Turing",
            "Martin",
            "Houdin",
            "Newell",
            "Volobuev"
        };
        
        private readonly List<string> roles = new List<string>
        {
            "Manager",
            "Programmer",
            "Producer",
            "QA",
            "Designer",
            "Owner"
        };

        private Random random;
        private int workerCount;
        
        public override void Execute()
        {
            random = new Random();
            CreateRandomWorkers();
            var count = random.Next(6, 12);
            for (var i = 0; i < count; i++)
            {
                var project = new Project
                {
                    Name = RandomElement(projectNames),
                    Cost = (float) (50000 * random.NextDouble()),
                    Start = RandomDay(),
                    IsCompleted = random.Next(2) == 1,
                    Workers = RandomElements(ProjectsHolder.Workers)
                };
                project.End = project.IsCompleted ? RandomDayStartingAt(project.Start) : project.Start;
                ProjectsHolder.RegisterProject(project);
            }
        }
        
        private void CreateRandomWorkers()
        {
            ProjectsHolder.Workers.AddRange(Enumerable.Range(0, random.Next(3, 12)).Select(_ => CreateWorker()));
        }
        
        private Worker CreateWorker()
        {
            return new Worker
            {
                Id = workerCount++,
                FirstName = RandomElement(workersNames),
                SecondName = RandomElement(secondNames),
                Role = RandomElement(roles)
            };
        }

        private T RandomElement<T>(List<T> list)
        {
            return list[random.Next(list.Count)];
        }
        
        private List<T> RandomElements<T>(List<T> list)
        {
            var tmp = list.ToList();
            for (var i = 0; i < tmp.Count; i++)
            {
                var index = random.Next(tmp.Count);
                var el = tmp[i];
                tmp[i] = tmp[index];
                tmp[index] = el;
            }
            return tmp.Take(random.Next(1, 3)).ToList();
        }
        
        private DateTime RandomDay()
        {
            return RandomDayStartingAt(new DateTime(2000, 1, 1));
        }
        
        private DateTime RandomDayStartingAt(DateTime start)
        {
            var range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}