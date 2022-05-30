using GitManager.Business.Helpers.Interfaces;
using GitManager.Business.Helpers;
using GitManager.ViewModels;
using CsharpTools.Services.Interfaces;
using CsharpTools.Services;
using GitManager.Business;

namespace GitManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            #region ViewModels
            builder.Services.AddSingleton<HomeViewModel>();
            #endregion

            #region Services
            builder.Services.AddSingleton<IFileService, FileService>();
            builder.Services.AddSingleton<ILogService, LogService>();
            builder.Services.AddSingleton<TerminalService>();
            #endregion

            #region Helpers
            builder.Services.AddSingleton<IGitHelper, GitHelper>();
            #endregion

            return builder.Build();
        }
    }
}