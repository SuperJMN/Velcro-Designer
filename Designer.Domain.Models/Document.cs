using System.Collections.Generic;

namespace Designer.Domain.Models
{
    public class Document
    {
        public IList<Item> Items { get; set; }

        public string Name { get; set; }
    }
}