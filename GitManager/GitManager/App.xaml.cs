using GitManager.Business.Helpers.Interfaces;
using GitManager.ViewModels;

namespace GitManager
{
    public partial class App : Application
    {
        public static IServiceProvider serviceProvider { get; set; }

        private readonly IGitHelper _gitHelper;

        public App(IServiceProvider sp, IGitHelper gitHelper)
        {
            InitializeComponent();
            serviceProvider = sp;
            _gitHelper = gitHelper;
            MainPage = new AppShell();
        }


        protected override void OnSleep()
        {
            var homeViewModel = (HomeViewModel)App.serviceProvider.GetService(typeof(HomeViewModel));
            var configurations = homeViewModel.GitConfigurations;
            _gitHelper.SaveConfigurations(configurations);
            base.OnSleep();
        }
    }
}