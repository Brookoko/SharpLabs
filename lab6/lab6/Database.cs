namespace lab6
{
    using System.Data;
    using DependencyInjection;
    using Devart.Data.PostgreSql;
    
    public class Database
    {
        private const string ConnectionString =
            "user id=postgres;password=postgres;host=localhost;port=5432;database=c#;schema=public";
        
        private PgSqlConnection connection;
        
        [PostConstruct]
        public void Prepare()
        {
            connection = new PgSqlConnection(ConnectionString);
            connection.Open();
        }
        
        public DataSet ExecuteCommand(PgSqlCommand command)
        {
            command.Connection = connection;
            var adapter = new PgSqlDataAdapter(command);
            var ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
        
        ~Database()
        {
            connection.Close();
        }
    }
}