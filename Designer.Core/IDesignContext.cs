using System.Collections.Generic;

namespace Designer.Core
{
    public interface IDesignContext
    {
        ICollection<Node> Nodes { get; set; }
        ICollection<Node> Selection { get; set; }
    }
}