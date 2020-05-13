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
        
        public DataSet CompletedProjects()
        {
            var sql = "select * from orders where enddate < now()";
            var command = new PgSqlCommand(sql);
            return Database.ExecuteCommand(command);
        }
        
        public DataSet ProjectsOrderByStart()
        {
            var sql = "select * from orders order by startdate";
            var command = new PgSqlCommand(sql);
            return Database.ExecuteCommand(command);
        }
        
        public DataSet GetAllWorkersInDepartment(string name)
        {
            var sql = @"select FirstName, SecondName, Title from employees where DepartmentId in (
                select DepartmentId from departments where Name = @name
            )";
            var command = new PgSqlCommand(sql);
            command.Parameters.AddWithValue("@name", name);
            return Database.ExecuteCommand(command);
        }
        
        public DataSet WorkOnMostProjects()
        {
            var sql = @"select FirstName, SecondName, Title, count(EmployeeId) as Projects from employees
                left join orderdetails on Array[EmployeeId] && EmployeeIds
                group by EmployeeId
                order by count(EmployeeId) desc limit 1";
            var command = new PgSqlCommand(sql);
            return Database.ExecuteCommand(command);
        }

        public DataSet WorkingOn(int id)
        {
            var sql = @"select o.* from orders o
                left join orderdetails d on o.OrderId = d.OrderId
                where EndDate < now() and @id = any(EmployeeIds)";
            var command = new PgSqlCommand(sql);
            command.Parameters.AddWithValue("@id", id);
            return Database.ExecuteCommand(command);
        }

        public DataSet IncomingFrom(int id)
        {
            var sql = @"select CustomerId, sum(Cost) from orders
                where CustomerId = @id
                group by CustomerId";
            var command = new PgSqlCommand(sql);
            command.Parameters.AddWithValue("@id", id);
            return Database.ExecuteCommand(command);
        }
        
        public DataSet Legal()
        {
            var sql = @"select Name from customers where Type = 'l'";
            var command = new PgSqlCommand(sql);
            return Database.ExecuteCommand(command);
        }
        
        public DataSet MostProductive()
        {
            var sql = @"select Name from Departments where DepartmentId = (
                select DepartmentId as Projects from employees e
                left join orderdetails o on Array[EmployeeId] && EmployeeIds
                group by DepartmentId
                order by count(DepartmentId) desc limit 1
            )";
            var command = new PgSqlCommand(sql);
            return Database.ExecuteCommand(command);
        }
        
        public DataSet CostOfProjects()
        {
            var sql = @"select sum(cost) from orders";
            var command = new PgSqlCommand(sql);
            return Database.ExecuteCommand(command);
        }
        
        public DataSet Create(string name, char type)
        {
            var sql = @"insert into customers(Name, Type) VALUES (@name, @type);";
            var command = new PgSqlCommand(sql);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@type", type);
            Database.ExecuteCommand(command);
            return Customers();
        }
        
        public DataSet Delete(int id)
        {
            var sql = @"delete from customers where CustomerId = @id";
            var command = new PgSqlCommand(sql);
            command.Parameters.AddWithValue("@id", id);
            Database.ExecuteCommand(command);
            return Customers();
        }
        
        public DataSet Customers()
        {
            var sql = @"select * from customers";
            var command = new PgSqlCommand(sql);
            return Database.ExecuteCommand(command);
        }
    }
}