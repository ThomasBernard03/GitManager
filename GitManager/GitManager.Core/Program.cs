using GitManager.Core.Helpers;
using GitManager.Core.Helpers.Interfaces;

namespace GitManager
{
    class Program
    {
        private static IGitHelper _gitHelper;
        async static Task Main(string[] args)
        {
            _gitHelper = new GitHelper();

            var gitConfiguration = _gitHelper.GetConfiguration();
        }
    }
}
