using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SqlToolbelt.Impl
{
    public class SqlParser : ISqlParser
    {
        public IEnumerable<string> Parse(string batch)
        {
            const string pattern = @"(^\s*GO\s*$)";
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;
            IEnumerable<string> list = Regex.Split(batch, pattern, options);
            return list
                .Select(s => s.TrimStart('\n', '\r'))
                .Select(s => s.TrimEnd('\n', '\r'))
                .Select(s => s.Trim())
                .Where(p => !p.Equals("GO") && !string.IsNullOrWhiteSpace(p));
        }
    }
}