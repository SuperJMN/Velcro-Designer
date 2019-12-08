using System.Collections.Generic;

namespace Designer.Domain.ViewModels
{
    public interface IDesignContext
    {
        ICollection<Item> Nodes { get; set; }
        ICollection<Item> Selection { get; set; }
        IDesignCommandsHost DesignCommandsHost { get; set; }
    }
}