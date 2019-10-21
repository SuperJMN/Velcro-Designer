using System.IO;
using System.Threading.Tasks;

namespace Designer.Core
{
    public interface IProjectStore
    {
        Task Save(Project project, Stream stream);
        Task<Project> Load(Stream stream);
    }
}