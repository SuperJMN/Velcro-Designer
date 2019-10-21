using System;
using System.Linq;

namespace Designer.Model.Tests
{
    public class Section
    {
        public string Header { get; }
        public Content Content { get; }

        public Section(string header, Content content)
        {
            Header = header;
            Content = content;
        }

        public override string ToString()
        {
            var a = new[] { Header.ToString() };
            var b = new[] { Content.ToString() };

            var str = string.Join(Environment.NewLine, a.Concat(b));
            return str;
        }
    }
}