using System;
using System.Linq;

namespace Designer.Model.Tests
{
    public class Resource
    {
        public string Name { get; }
        public Statement[] Statements { get; }

        public Resource(string name, Statement[] statements)
        {
            Name = name;
            Statements = statements;
        }

        public override string ToString()
        {
            var str = string.Join(Environment.NewLine, new[] { $"{{{Name}}}" }.Concat(Statements.Select(x => x.ToString())));
            return str;
        }
    }
}