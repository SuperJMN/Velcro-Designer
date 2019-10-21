using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Designer.Core.Persistence
{
    public interface IImportPlugin
    {
        string FileExtension { get; }
        Task<IList<Document>> Load(Stream stream);        Task Save(Stream stream, IList<Document> documents);    }
}