using CsharpTools.Services;
using GitManager.Core.Models;
using Newtonsoft.Json;

namespace GitManager.Core.Services
{
    public class GitConfigurationService
    {
        private readonly FileService _fileService;
        private string _configurationsFilePath = Directory.GetCurrentDirectory();
        private string _configurationFileName = "configurations.json";
        private string _configurationFullPath => $"{_configurationsFilePath}\\{_configurationFileName}";

        public GitConfigurationService()
        {
            _fileService = new FileService();
        }


        public void AddGitConfiguration(GitConfiguration gitConfiguration)
        {
            // 1) Get all configs
            var configurations = new List<GitConfiguration>();

            var existingConfigurations = GetGitConfigurations();

            if (existingConfigurations != null)
                configurations.AddRange(existingConfigurations);

            configurations.Add(gitConfiguration);

            // 2) Delete File (Create a new one)
            _fileService.CreateFile(_configurationsFilePath, _configurationFileName);

            // 3) Add content
            _fileService.AppendContent(_configurationFullPath, JsonConvert.SerializeObject(configurations));
        }

        public List<GitConfiguration> GetGitConfigurations()
        {
            return JsonConvert.DeserializeObject<List<GitConfiguration>>(File.ReadAllText(_configurationFullPath));
        }
    }
}
