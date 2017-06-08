using System;
using System.IO;

namespace SqlToolbelt.Impl
{
    public class SqlFileExecutor : ISqlFileExecutor
    {
        private readonly ISqlParser _parser;
        private readonly ISqlExecutor _sqlExecutor;

        public SqlFileExecutor(ISqlParser parser, ISqlExecutor sqlExecutor)
        {
            _parser = parser;
            _sqlExecutor = sqlExecutor;
        }

        public void ExecuteFile(string path)
        {
            _sqlExecutor.Execute(_parser.Parse(File.ReadAllText(path)));
        }

        public void ExecuteFiles(string directory)
        {
            ExecuteFiles(directory, _ => true);
        }

        public void ExecuteFiles(string directory, Predicate<string> filter)
        {
            _ExecuteFromFolder(directory, filter);
        }

        private void _ExecuteFromFolder(string path, Predicate<string> predicate)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (predicate(file))
                {
                    ExecuteFile(file);
                }
            }
            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                _ExecuteFromFolder(directory, predicate);
            }
        }
    }
}