using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SqlToolbelt.Impl;

namespace SqlToolbelt.Tests
{
    [ExcludeFromCodeCoverage]
    internal class SqlFileExecutorShould
    {
        private readonly string _directory = AppDomain.CurrentDomain.BaseDirectory;

        [Test]
        public void execute_a_file()
        {
            var executor = Substitute.For<ISqlExecutor>();
            var batchExecutor = new SqlFileExecutor(new SqlParser(), executor);
            var path = CreateFile("Foo.txt", "Foo");
            batchExecutor.ExecuteFile(path);
            executor.Received().Execute(Arg.Is<IEnumerable<string>>(e => e.SequenceEqual(new[] { "Foo" })));
            DeleteFile(path);
        }

        [Test]
        public void execute_a_directory()
        {
            var executor = Substitute.For<ISqlExecutor>();
            var batchExecutor = new SqlFileExecutor(new SqlParser(), executor);
            CreateFile("Foo\\Bar.txt", "Bar");
            CreateFile("Foo\\Baz.txt", "Baz");
            CreateFile("Foo\\Qux\\Quux.txt", "Quux");
            var directory = Path.Combine(_directory, "Foo");
            batchExecutor.ExecuteFiles(directory);
            executor.Received(3).Execute(Arg.Any<IEnumerable<string>>());
            var receivedCalls = executor.ReceivedCalls();
            receivedCalls.Any(call => ((IEnumerable<string>)call.GetArguments()[0]).SequenceEqual(new[] { "Bar" })).Should().BeTrue();
            receivedCalls.Any(call => ((IEnumerable<string>)call.GetArguments()[0]).SequenceEqual(new[] { "Baz" })).Should().BeTrue();
            receivedCalls.Any(call => ((IEnumerable<string>)call.GetArguments()[0]).SequenceEqual(new[] { "Quux" })).Should().BeTrue();
            DeleteDirectory(directory);
        }

        [Test]
        public void execute_a_directory_filtering_files()
        {
            var executor = Substitute.For<ISqlExecutor>();
            var batchExecutor = new SqlFileExecutor(new SqlParser(), executor);
            CreateFile("Foo\\Bar.txt", "Bar");
            CreateFile("Foo\\Baz.txt", "Baz");
            CreateFile("Foo\\Qux\\Quux.txt", "Quux");
            var directory = Path.Combine(_directory, "Foo");
            Predicate<string> predicate = file => Path.GetFileName(file) != "Quux.txt";
            batchExecutor.ExecuteFiles(directory, predicate);
            executor.Received(2).Execute(Arg.Any<IEnumerable<string>>());
            var receivedCalls = executor.ReceivedCalls();
            receivedCalls.Any(call => ((IEnumerable<string>)call.GetArguments()[0]).SequenceEqual(new[] { "Bar" })).Should().BeTrue();
            receivedCalls.Any(call => ((IEnumerable<string>)call.GetArguments()[0]).SequenceEqual(new[] { "Baz" })).Should().BeTrue();
            DeleteDirectory(directory);
        }

        private string CreateFile(string fileName, string content)
        {
            var path = Path.Combine(_directory, fileName);
            new FileInfo(path).Directory.Create();
            File.WriteAllText(path, content);
            return path;
        }

        private static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        private static void DeleteDirectory(string path)
        {
            Directory.Delete(path, recursive: true);
        }
    }
}