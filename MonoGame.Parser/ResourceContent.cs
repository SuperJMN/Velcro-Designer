using System;
using System.Linq;

namespace Designer.Model.Tests
{
    class ResourceContent : Content
    {
        public Resource[] Resources { get; }

        public ResourceContent(Resource[] resources)
        {
            Resources = resources;
        }

        public override string ToString()
        {
            var str = string.Join(Environment.NewLine, Resources.Select(x => x.ToString()));
            return str;
        }
    }
}