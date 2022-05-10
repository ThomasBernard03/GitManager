using ToolBox.Bridge;
using ToolBox.Platform;

namespace GitManager.Core.Services
{
    public class TerminalService
    {
        public static IBridgeSystem _bridgeSystem { get; set; }
        public static ShellConfigurator _shell { get; set; }

        public TerminalService()
        {
            switch (OS.GetCurrent())
            {
                case "win":
                    _bridgeSystem = BridgeSystem.Bat;
                    break;
                case "mac":
                case "gnu":
                    _bridgeSystem = BridgeSystem.Bash;
                    break;
            }
            _shell = new ShellConfigurator(_bridgeSystem);
        }
    }
}
