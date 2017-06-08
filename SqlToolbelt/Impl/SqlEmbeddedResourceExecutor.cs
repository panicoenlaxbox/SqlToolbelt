using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SqlToolbelt.Impl
{
    public class SqlEmbeddedResourceExecutor : ISqlEmbeddedResourceExecutor
    {
        private readonly ISqlParser _parser;
        private readonly ISqlExecutor _sqlExecutor;

        public SqlEmbeddedResourceExecutor(ISqlParser parser, ISqlExecutor sqlExecutor)
        {
            _parser = parser;
            _sqlExecutor = sqlExecutor;
        }

        public void ExecuteEmbeddedResource(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                _sqlExecutor.Execute(_parser.Parse(reader.ReadToEnd()));
            }
        }

        public void ExecuteEmbeddedResources(Assembly assembly, Predicate<string> filter)
        {
            var names = assembly.GetManifestResourceNames();
            foreach (var name in names)
            {
                if (filter(name))
                {
                    using (var stream = assembly.GetManifestResourceStream(name))
                    using (var reader = new StreamReader(stream))
                    {
                        _sqlExecutor.Execute(_parser.Parse(reader.ReadToEnd()));
                    }
                }
            }
        }
    }
}