using System.Collections.Generic;

namespace SqlToolbelt
{
    public interface ISqlParser
    {
        IEnumerable<string> Parse(string batch);
    }
}