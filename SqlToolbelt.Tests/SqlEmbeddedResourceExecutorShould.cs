using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SqlToolbelt.Impl;

namespace SqlToolbelt.Tests
{
    [ExcludeFromCodeCoverage]
    internal class SqlEmbeddedResourceExecutorShould
    {
        [Test]
        public void execute_a_embedded_resource()
        {
            var executor = Substitute.For<ISqlExecutor>();
            var embeddedResourceExecutor = new SqlEmbeddedResourceExecutor(new SqlParser(), executor);
            var resourceName = $"SqlToolbelt.Tests.{nameof(execute_a_embedded_resource)}.Foo.txt";
            embeddedResourceExecutor.ExecuteEmbeddedResource(Assembly.GetExecutingAssembly(), 
                resourceName);
            executor.Received().Execute(Arg.Is<IEnumerable<string>>(e => e.SequenceEqual(new[] { "Foo" })));
        }

        [Test]
        public void execute_a_list_of_embedded_resources()
        {
            var executor = Substitute.For<ISqlExecutor>();
            var embeddedResourceExecutor = new SqlEmbeddedResourceExecutor(new SqlParser(), executor);
            embeddedResourceExecutor.ExecuteEmbeddedResources(
                Assembly.GetExecutingAssembly(), s => s.Contains(nameof(execute_a_list_of_embedded_resources)));
            executor.Received(2).Execute(Arg.Any<IEnumerable<string>>());
            var receivedCalls = executor.ReceivedCalls();
            receivedCalls.Any(call => ((IEnumerable<string>)call.GetArguments()[0]).SequenceEqual(new[] { "Foo" })).Should().BeTrue();
            receivedCalls.Any(call => ((IEnumerable<string>)call.GetArguments()[0]).SequenceEqual(new[] { "Bar" })).Should().BeTrue();
        }       
    }
}