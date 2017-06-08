using System;

namespace SqlToolbelt
{
    public interface ISqlFileExecutor
    {
        void ExecuteFile(string path);
        void ExecuteFiles(string directory);
        void ExecuteFiles(string directory, Predicate<string> filter);
    }
}