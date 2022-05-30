using CsharpTools.Services;
using CsharpTools.Services.Interfaces;
using GitManager.Business.Helpers.Interfaces;
using GitManager.Models;

namespace GitManager.Business.Helpers
{
    public class GitHelper : IGitHelper
    {
        private readonly IFileService _fileService;
        private readonly ILogService _logService;

        public GitHelper(IFileService fileService, ILogService logService)
        {
            _fileService = fileService;
            _logService = logService;
            _logService.DirectoryPath = $"{AppContext.BaseDirectory}\\Logs";
            new TerminalService();
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

            _logService.Info($"User get {scope} configuration (returned {result.Email})");

            return result;
        }

        public void SetConfiguration(GitConfiguration gitConfiguration, GitConfigurationScope scope)
        {
            //var command = $"git config --{scope} user.name \"{gitConfiguration.Name}\"";
            var command = $"git config --{scope} user.name \"{gitConfiguration.Name}\" --replace-all & git config --{scope} user.email {gitConfiguration.Email} --replace-all";
            var commandResult = TerminalService.Shell.Term(command);
            _logService.Info($"User set new {scope} configuration ({gitConfiguration.Email})");
        }


        public IEnumerable<GitConfiguration> GetSavedConfigurations()
        {
            _logService.Info("User get all saved configurations");
            return new List<GitConfiguration>();
        }
    }
}
