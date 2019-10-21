using System;
using System.Linq;

namespace Designer.Model.Tests
{
    class StatementContent : Content
    {
        public Statement[] Statements { get; }

        public StatementContent(Statement[] statements)
        {
            Statements = statements;
        }

        public override string ToString()
        {
            var str = string.Join(Environment.NewLine, Statements.Select(x => x.ToString()));
            return str;
        }
    }
}