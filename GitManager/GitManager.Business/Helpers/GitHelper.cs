using CsharpTools.Services;
using CsharpTools.Services.Interfaces;
using GitManager.Business.Helpers.Interfaces;
using GitManager.Models;
using Newtonsoft.Json;

namespace GitManager.Business.Helpers
{
    public class GitHelper : IGitHelper
    {
        private readonly IFileService _fileService;
        private readonly ILogService _logService;

        private static string _configurationsFilePath = $"{AppContext.BaseDirectory}";
        private static string _configurationFileName = "configurations.json";
        private static string _configurationsFileFullPath = $"{_configurationsFilePath}{_configurationFileName}";

        public GitHelper(IFileService fileService, ILogService logService)
        {
            _fileService = fileService;
            _logService = logService;
            _logService.DirectoryPath = $"{AppContext.BaseDirectory}\\Logs";
            new TerminalService();

            if (!File.Exists(_configurationsFileFullPath))
                _fileService.CreateFile(AppContext.BaseDirectory, _configurationFileName);
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
            var resetCommand = "git config --global --unset-all user.name & git config --global--unset - all user.email";
            TerminalService.Shell.Term(resetCommand);

            //var command = $"git config --{scope} user.name \"{gitConfiguration.Name}\"";
            var command = $"git config --{scope} user.name \"{gitConfiguration.Name}\" --replace-all & git config --{scope} user.email {gitConfiguration.Email} --replace-all";
            var commandResult = TerminalService.Shell.Term(command);
            _logService.Info($"User set new {scope} configuration ({gitConfiguration.Email})");
        }


        public IEnumerable<GitConfiguration> GetSavedConfigurations()
        {
            try
            {
                var fileContent = File.ReadAllText(_configurationsFileFullPath);

                _logService.Info("User get all saved configurations");
                var gitConfigurations = JsonConvert.DeserializeObject<IEnumerable<GitConfiguration>>(fileContent);
                return gitConfigurations == null ? new List<GitConfiguration>() : gitConfigurations;
            }
            catch (Exception ex)
            {
                _logService.Error(ex);
                return null;
            }
        }


        /// <summary>
        /// Delete all configurations and replace it by gitConfigurations
        /// </summary>
        /// <param name="gitConfigurations">New configurations</param>
        public void SaveConfigurations(IEnumerable<GitConfiguration> gitConfigurations)
        {
            try
            {
                _fileService.RemoveFile(_configurationsFileFullPath);
                _fileService.CreateFile(_configurationsFilePath, _configurationFileName);

                _fileService.AppendContent(_configurationsFileFullPath, JsonConvert.SerializeObject(gitConfigurations));
            }
            catch (Exception ex)
            {
                _logService.Error(ex);
            }
        }

    }
}
