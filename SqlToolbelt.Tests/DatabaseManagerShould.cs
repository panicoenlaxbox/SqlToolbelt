using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;
using SqlToolbelt.Impl;

namespace SqlToolbelt.Tests
{
    [ExcludeFromCodeCoverage]
    internal class DatabaseManagerShould
    {
        private const string Database = "Foo";

        [Test]
        public void create_a_database()
        {
            var manager = CreateDatabaseManager();
            manager.CreateDatabase(Database);
        }

        [Test]
        public void drop_a_database()
        {
            var manager = CreateDatabaseManager();            
            manager.CreateDatabase(Database);
            manager.DropDatabase(Database);
        }

        private static DatabaseManager CreateDatabaseManager()
        {
            return new DatabaseManager(
                new SqlExecutor("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Trusted_Connection=yes;", "System.Data.SqlClient"));
        }
    }
}