using GitManager.Models;

namespace GitManager.Business.Helpers.Interfaces
{
    public interface IGitHelper
    {
        /// <summary>
        /// Get the git configuation
        /// </summary>
        /// <returns>The git configuration</returns>
        GitConfiguration GetConfiguration(GitConfigurationScope scope = GitConfigurationScope.global);

        /// <summary>
        /// Set the git configuration
        /// </summary>
        /// <param name="gitConfiguration">The git configutation</param>
        /// <param name="scope">The scope to apply</param>
        void SetConfiguration(GitConfiguration gitConfiguration, GitConfigurationScope scope);

        /// <summary>
        /// Get all saved configurations saved in configurations.json
        /// </summary>
        /// <returns>All saved Configuration</returns>
        IEnumerable<GitConfiguration> GetSavedConfigurations();
    }
}
