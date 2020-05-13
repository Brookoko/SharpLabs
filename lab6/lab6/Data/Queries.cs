namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using DependencyInjection;
    using Devart.Data.PostgreSql;
    using lab6;

    public class Queries
    {
        [Inject]
        public Database Database { get; set; }
        
        public DataSet AllProjects()
        {
            var sql = "select * from orders";
            var command = new PgSqlCommand(sql);
            return Database.ExecuteCommand(command);
        }
        
        public IEnumerable<Project> CompletedProjects()
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Project> ProjectsOrderByStart()
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Project> StartBefore(DateTime time)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Project> StartAfter(DateTime time)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> InRange(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Project FirstProject()
        {
            throw new NotImplementedException();
        }
        
        public float CostOfProjects()
        {
            throw new NotImplementedException();
        }

        public Project LastProjectOf(Worker worker)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> CurrentlyWorkingOn(Worker worker)
        {
            throw new NotImplementedException();
        }
        
        public string CommonName()
        {
            throw new NotImplementedException();
        }

        public Worker WorkOnMostProjects()
        {
            throw new NotImplementedException();
        }

        public int ProjectCount(Worker worker)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Worker> AllWorkers()
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Worker> WorkersWithName(string name)
        {
            throw new NotImplementedException();
        }
    }
}