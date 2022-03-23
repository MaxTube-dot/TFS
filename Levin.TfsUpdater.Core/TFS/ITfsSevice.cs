
using Levin.TfsUpdater.Core.TFS;
using Microsoft.TeamFoundation.TestManagement.WebApi;

namespace Levin.TfsUpdater.Core
{
    public interface ITfsSevice
    {
        WorkTask[] GetItems(string wiql);


    }
}
