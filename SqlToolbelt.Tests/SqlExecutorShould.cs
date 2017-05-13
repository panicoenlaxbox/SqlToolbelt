using System.Data;
using NSubstitute;
using NUnit.Framework;
using SqlToolbelt.Impl;

namespace SqlToolbelt.Tests
{
    internal class SqlExecutorShould
    {
        [Test]
        public void open_and_close_connection_if_it_is_closed()
        {
            var connection = Substitute.For<IDbConnection>();
            var executor = new SqlExecutor(connection);
            executor.Execute(new[] { "Sergio", "panicoenlaxbox" });
            connection.Received().Open();
            connection.Received().Close();
        }

        [Test]
        public void neither_open_nor_close_connection_if_it_is_open()
        {
            var connection = Substitute.For<IDbConnection>();
            connection.State.Returns(ConnectionState.Open);
            var executor = new SqlExecutor(connection);
            executor.Execute(new[] { "Sergio", "panicoenlaxbox" });
            connection.DidNotReceive().Open();
            connection.DidNotReceive().Close();
        }
    }
}