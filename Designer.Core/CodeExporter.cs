using System.IO;
using System.Threading.Tasks;
using Designer.Domain.Models;

namespace Designer.Core
{
    public class CodeExporter : IExporter
    {
        public Task Export(Project project, Stream stream)
        {
            return Task.CompletedTask;
        }
    }
}