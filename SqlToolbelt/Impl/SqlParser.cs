using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SqlToolbelt.Impl
{
    public class SqlParser : ISqlParser
    {
        public IEnumerable<string> Parse(string batch)
        {
            const string pattern = @"(^GO)|(\n{1}GO)";
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;
            return Regex.Split(batch, pattern, options)
                .Where(p => !p.Equals("GO") && !p.Equals("\nGO") && !string.IsNullOrWhiteSpace(p))
                .Select(s => s.TrimStart('\n', '\r'))
                .Select(s => s.TrimEnd('\n', '\r'))
                .Select(s => s.Trim());
        }
    }
}