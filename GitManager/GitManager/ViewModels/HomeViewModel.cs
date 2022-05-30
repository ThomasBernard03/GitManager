using GitManager.Business.Helpers.Interfaces;
using GitManager.Models;

namespace GitManager.ViewModels;

public class HomeViewModel
{
    public List<GitConfiguration> GitConfigurations { get; set; }
    public GitConfiguration ActiveGitConfiguration { get; set; }

    public HomeViewModel(IGitHelper gitHelper)
    {
        ActiveGitConfiguration = gitHelper.GetConfiguration();
    }



}
