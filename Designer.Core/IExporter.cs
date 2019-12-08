using System.IO;
using System.Threading.Tasks;

namespace Designer.Core
{
    public interface IExporter
    {
        Task Export(Domain.Models.Project project, Stream stream);
    }
}