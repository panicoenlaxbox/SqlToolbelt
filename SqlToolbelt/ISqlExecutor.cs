using System.Collections.Generic;

namespace SqlToolbelt
{
    public interface ISqlExecutor
    {
        void Execute(string sql);
        void Execute(IEnumerable<string> batch);
    }
}