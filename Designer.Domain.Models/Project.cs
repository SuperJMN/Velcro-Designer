using System.Collections.Generic;

namespace Designer.Domain.Models
{
    public class Project
    {
        public ICollection<Document> Documents { get; set; }
    }
}