namespace SqlToolbelt.Impl
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly ISqlExecutor _sqlExecutor;

        public DatabaseManager(ISqlExecutor sqlExecutor)
        {
            _sqlExecutor = sqlExecutor;
        }

        public void CreateDatabase(string databaseName)
        {
            var sql = $@"
                IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}') 
                BEGIN
                    CREATE DATABASE [{databaseName}]
                END";
            _sqlExecutor.Execute(sql);
        }

        public void DropDatabase(string databaseName)
        {
            var sql =$@"
                IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}')
                BEGIN
                    ALTER DATABASE [{databaseName}] SET single_user WITH ROLLBACK IMMEDIATE
                    DROP DATABASE [{databaseName}]
                END";
            _sqlExecutor.Execute(sql);
        }
    }
}