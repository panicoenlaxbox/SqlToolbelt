using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SqlToolbelt.Impl
{
    public class SqlExecutor : ISqlExecutor
    {
        private readonly IDbConnection _connection;

        public SqlExecutor(IDbConnection connection)
        {
            _connection = connection;
        }

        public SqlExecutor(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Execute(string sql)
        {
            Execute(new[] { sql });
        }

        public void Execute(IEnumerable<string> batch)
        {
            var closeConnection = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                closeConnection = true;
            }
            try
            {
                foreach (var sql in batch)
                {
                    using (var command = _connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                if (closeConnection)
                {
                    _connection.Close();
                }
            }
        }
    }
}