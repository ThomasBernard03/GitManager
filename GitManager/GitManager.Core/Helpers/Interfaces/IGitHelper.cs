using GitManager.Core.Models;

namespace GitManager.Core.Helpers.Interfaces
{
    public interface IGitHelper
    {
        /// <summary>
        /// Get the git configuation
        /// </summary>
        /// <returns>The git configuration</returns>
        GitConfiguration GetConfiguration(GitConfigurationScope scope = GitConfigurationScope.global);
    }
}
