
using System.Collections.Generic;

namespace Designer.Model
{
    public class Document
    {
        public IList<Node> Graphics { get; set; }
        public string Name { get; set; }
    }
}