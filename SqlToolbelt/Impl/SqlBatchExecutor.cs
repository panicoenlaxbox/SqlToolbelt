using System.IO;
using System.Reflection;

namespace SqlToolbelt.Impl
{
    public class SqlBatchExecutor :  ISqlBatchExecutor
    {
        private readonly ISqlParser _parser;
        private readonly ISqlExecutor _sqlExecutor;

        public void ExecuteFromEmbeddedResource(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                _sqlExecutor.Execute(_parser.Parse(reader.ReadToEnd()));
            }
        }

        public void ExecuteFromFile(string path)
        {
            _sqlExecutor.Execute(_parser.Parse(File.ReadAllText(path)));
        }

        public SqlBatchExecutor(ISqlParser parser, ISqlExecutor sqlExecutor)
        {
            _parser = parser;
            _sqlExecutor = sqlExecutor;
        }
    }
}