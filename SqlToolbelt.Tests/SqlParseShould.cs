using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;
using SqlToolbelt.Impl;

namespace SqlToolbelt.Tests
{
    [ExcludeFromCodeCoverage]
    internal class SqlParseShould
    {
        [TestCaseSource(typeof(ParseSuccessfullyTestDataSource), nameof(ParseSuccessfullyTestDataSource.GetValues))]
        public void parse_sql_batch(string batch, string[] expected)
        {
            var parser = new SqlParser();
            parser.Parse(batch).Should().Equal(expected);
        }      

        private class ParseSuccessfullyTestDataSource
        {
            public static IEnumerable<TestCaseData> GetValues()
            {
                yield return new TestCaseData("Sergio\nGO\npanicoenlaxbox\nGO", new[] { "Sergio", "panicoenlaxbox" })
                    .SetName("a_canonical_example");
                yield return new TestCaseData("GO\nGO\npanicoenlaxbox\nGO\nSergio", new[] { "panicoenlaxbox", "Sergio" })
                    .SetName("a_odd_example");
            }
        }
    }
}