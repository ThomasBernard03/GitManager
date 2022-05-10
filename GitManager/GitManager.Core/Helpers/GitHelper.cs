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

        public GitConfiguration GetConfiguration(GitConfigurationScope scope = GitConfigurationScope.global)
        {
            var commandResult = TerminalService.Shell.Term($"git config --{scope} user.name & git config --{scope} user.email");

            var gitNameAndMail = commandResult.stdout.Split("\n").Take(2);

            var result = new GitConfiguration()
            {
                Name = gitNameAndMail.FirstOrDefault(),
                Email = gitNameAndMail.LastOrDefault(),
                Scope = scope,
            };

            return result;
        }
    }
}
