using ToolBox.Bridge;
using ToolBox.Platform;

namespace GitManager.Core.Services
{
    public class TerminalService
    {
        public static IBridgeSystem BridgeSystem { get; set; }
        public static ShellConfigurator Shell { get; set; }

        public TerminalService()
        {
            switch (OS.GetCurrent())
            {
                case "win":
                    BridgeSystem = ToolBox.Bridge.BridgeSystem.Bat;
                    break;
                case "mac":
                case "gnu":
                    BridgeSystem = ToolBox.Bridge.BridgeSystem.Bash;
                    break;
            }
            Shell = new ShellConfigurator(BridgeSystem);
        }
    }
}
