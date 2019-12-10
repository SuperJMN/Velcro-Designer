using System.Collections.Generic;

namespace Designer.Domain.Models
{
    public class Document
    {
        public IEnumerable<Item> Graphics { get; set; }

        public string Name { get; set; }
    }
}