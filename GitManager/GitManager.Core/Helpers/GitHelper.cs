using GitManager.Core.Helpers.Interfaces;
using GitManager.Core.Models;
using GitManager.Core.Services;

namespace GitManager.Core.Helpers
{
    public class GitHelper : IGitHelper
    {
        private readonly TerminalService _terminalService;
        public GitHelper()
        {
            _terminalService = new TerminalService();
        }

        public GitConfiguration GetConfiguration(GitConfigurationScope scope = GitConfigurationScope.Global)
        {
            throw new NotImplementedException();
        }
    }
}
