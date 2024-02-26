using System.Collections.Generic;
using System.Threading.Tasks;
using Dexla.Common.Editor.Entities;
using Dexla.Common.Repository.Types.Interfaces;

namespace Dexla.Common.Editor.Interfaces;

public interface IEditorContext : IContext
{ 
    Task<IEnumerable<Deployment>> GetMostRecentEnvironmentDeployments(string projectId);
}
