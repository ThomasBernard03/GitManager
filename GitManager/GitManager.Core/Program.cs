using GitManager.Core.Helpers;
using GitManager.Core.Helpers.Interfaces;
using GitManager.Core.Services;
using static GitManager.Core.Services.ConsoleService;

namespace GitManager
{
    class Program
    {
        private static IGitHelper _gitHelper;

        private static void Init()
        {
            _gitHelper = new GitHelper();

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

        private static void DisplayOptions()
        {
            var options = new List<string>() { "Change global configuration", "Change local configuration", "Add new configuration", "Exit" };
            Console.CursorTop = 20;
            var selectedItem = DisplaySelect(options);
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
    }
}
