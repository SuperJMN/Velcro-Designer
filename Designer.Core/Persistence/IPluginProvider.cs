using System.Collections.Generic;
using System.Threading.Tasks;

namespace Designer.Core.Persistence
{
    public interface IPluginProvider
    {
        Task<IList<IImportPlugin>> GetPlugins();
    }
}