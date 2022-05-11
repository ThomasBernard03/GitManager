using CsharpTools.Services;
using CsharpTools.Services.Interfaces;
using GitManager.Core.Helpers;
using GitManager.Core.Helpers.Interfaces;
using GitManager.Core.Models;
using GitManager.Core.Services;
using Newtonsoft.Json;
using static GitManager.Core.Services.ConsoleService;

namespace GitManager
{
    class Program
    {
        private static IGitHelper _gitHelper;
        private static GitConfigurationService _gitConfigurationService;

        private static void Init()
        {
            _gitHelper = new GitHelper();
            _gitConfigurationService = new GitConfigurationService();

            Clear();
            Console.CursorVisible = false;
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
            var options = new List<string>() { "Change global configuration", "Change local configuration", "Add new configuration", "Delete configuration", "Exit" };
            Console.CursorTop = 15;
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
                case 3:
                default:
                    return;
            }
        }

        private static void LoadConfigurationsFromFile(GitConfigurationScope scope)
        {
            Clear();
            WriteLine("Choose the git configuraion to apply : ");
            var configurations = _gitConfigurationService.GetGitConfigurations();
            var options = configurations.Select(c => $"{c.Name} ({c.Email})").ToList();
            options.Add("Cancel");

            var selectedConfigurationIndex = DisplaySelect(options);

            if (!(selectedConfigurationIndex >= configurations.Count))
            {
                var selectedConfiguration = configurations[selectedConfigurationIndex];
                _gitHelper.SetConfiguration(selectedConfiguration, scope);
            }


            Main(null);
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

            _gitConfigurationService.AddGitConfiguration(gitConfiguration);
            

            Main(null);
        }
    }
}
