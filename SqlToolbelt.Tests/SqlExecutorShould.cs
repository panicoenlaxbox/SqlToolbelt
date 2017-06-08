using System.Data;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using SqlToolbelt.Impl;

namespace SqlToolbelt.Tests
{
    [ExcludeFromCodeCoverage]
    internal class SqlExecutorShould
    {
        [Test]
        public void open_and_close_connection_if_it_is_closed()
        {
            var connection = Substitute.For<IDbConnection>();
            connection.State.Returns(ConnectionState.Closed);
            var executor = new SqlExecutor(connection);
            executor.Execute(string.Empty);
            connection.Received().Open();
            connection.Received().Close();
        }

        [Test]
        public void neither_open_nor_close_connection_if_it_is_open()
        {
            var connection = Substitute.For<IDbConnection>();
            connection.State.Returns(ConnectionState.Open);
            var executor = new SqlExecutor(connection);
            executor.Execute(string.Empty);
            connection.DidNotReceive().Open();
            connection.DidNotReceive().Close();
        }

        [Test]
        public void create_a_connection()
        {
            new SqlExecutor("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Trusted_Connection=yes;","System.Data.SqlClient");
        }
    }
}