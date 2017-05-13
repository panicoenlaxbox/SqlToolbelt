using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SqlToolbelt.Impl;

namespace SqlToolbelt.Tests
{
    internal class SqlParseShould
    {
        [TestCaseSource(typeof(ParseSuccessfullyTestDataSource), nameof(ParseSuccessfullyTestDataSource.GetValues))]
        public void parse_successfully(string batch, string[] expected)
        {
            var parser = new SqlParser();
            parser.Parse(batch).Should().Equal(expected);
        }

        private class ParseSuccessfullyTestDataSource
        {
            public static IEnumerable<TestCaseData> GetValues()
            {
                yield return new TestCaseData(@"Sergio 
GO
panicoenlaxbox
GO", new[] { "Sergio", "panicoenlaxbox" })
                    .SetName("a_canonical_example");
                yield return new TestCaseData(@"GO 
GO
panicoenlaxbox


GO
    Sergio", new[] { "panicoenlaxbox", "Sergio" })
                    .SetName("a_odd_example");
            }
        }
    }
}