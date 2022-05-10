using CsharpTools.Services;
using CsharpTools.Services.Interfaces;
using GitManager.Core.Helpers;
using GitManager.Core.Helpers.Interfaces;
using GitManager.Core.Models;
using Newtonsoft.Json;
using static GitManager.Core.Services.ConsoleService;

namespace GitManager
{
    class Program
    {
        private static IGitHelper _gitHelper;
        private static IFileService _fileService;
        private static string _configurationsFilePath;
        private static List<GitConfiguration> _configurations = new List<GitConfiguration>();

        private static void Init()
        {
            _gitHelper = new GitHelper();
            _fileService = new FileService();


            if (!File.Exists(_configurationsFilePath)) 
                _configurationsFilePath = _fileService.CreateFile(Directory.GetCurrentDirectory(), "configurations.json");
            else
            {
                File.
            }
                _configurations = JsonConvert.DeserializeObject<List<GitConfiguration>>(File.ReadAllText(_configurationsFilePath));

            Clear();
            Console.CursorVisible = false;
            Console.WindowHeight = 25;
            Console.BufferHeight = 25;
            Console.Title = "Git manager";
        }
        async static Task Main(string[] args)
        {
            Init();

            GetAndDisplayGlobalConfiguration();
            DisplayOptions();

            Console.ReadLine();
        }

        private static void GetAndDisplayGlobalConfiguration()
        {
            var gitGlobalConfiguration = _gitHelper.GetConfiguration();

            WriteLine("Your git global configuration is :");
            Write("    Name : ");
            WriteLine(gitGlobalConfiguration.Name, ConsoleColor.Green);

            Write("    Mail : ");
            WriteLine(gitGlobalConfiguration.Email, ConsoleColor.Green);
        }

        private static void DisplayOptions()
        {
            var options = new List<string>() { "Change global configuration", "Change local configuration", "Add new configuration", "Exit" };
            Console.CursorTop = 20;
            var selectedItem = DisplaySelect(options);

            switch (selectedItem)
            {
                case 0:
                    LoadConfigurationsFromFile(GitConfigurationScope.global);
                    break;
                case 1:
                    LoadConfigurationsFromFile(GitConfigurationScope.local);
                    break;
                case 2:
                    SaveConfigurationInFile();
                    break;
                default:
                    return;
            }
        }

        private static void LoadConfigurationsFromFile(GitConfigurationScope scope)
        {
            var content = File.ReadAllText(_configurationsFilePath);
        }

        private static void SaveConfigurationInFile()
        {
            Clear();
            WriteLine("Enter your configuration name :");
            var name = Console.ReadLine();
            WriteLine("Enter your configuration mail :");
            var email = Console.ReadLine();

            var gitConfiguration = new GitConfiguration()
            {
                Name = name,
                Email = email,
                Scope = GitConfigurationScope.unset
            };
            _configurations.Add(gitConfiguration);
            File.WriteAllText(_configurationsFilePath, JsonConvert.SerializeObject(_configurations));

            Main(null);
        }
    }
}
