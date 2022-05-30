using GitManager.Business.Helpers.Interfaces;
using GitManager.Models;
using System.Collections.ObjectModel;
using System.Data;

namespace GitManager.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private readonly IGitHelper _gitHelper;
    public ObservableCollection<GitConfiguration> GitConfigurations { get; set; }


    public HomeViewModel(IGitHelper gitHelper)
    {
        _gitHelper = gitHelper;
        NewConfiguration = new GitConfiguration();
        ActiveGitConfiguration = _gitHelper.GetConfiguration();
        GitConfigurations = new ObservableCollection<GitConfiguration>(_gitHelper.GetSavedConfigurations());
        SaveNewConfigurationCommand = new Command(OnSaveNewConfigurationCommand);
        DeleteConfigurationCommand = new Command(OnDeleteConfigurationCommand);
        OpenNewProjectCommand = new Command(async () => await OnOpenNewProjectCommand());
    }


    #region SaveNewConfigurationCommand => OnSaveNewConfigurationCommand
    public Command SaveNewConfigurationCommand { get; set; }
    private void OnSaveNewConfigurationCommand()
    {
        GitConfigurations.Add(NewConfiguration);
        NewConfiguration = new GitConfiguration();
    }
    #endregion

    public Command OpenNewProjectCommand { get; set; }
    private async Task OnOpenNewProjectCommand()
    {

    }

    #region DeleteConfigurationCommand => OnDeleteConfigurationCommand
    public Command DeleteConfigurationCommand { get; set; }
    private void OnDeleteConfigurationCommand()
    {
    }
    #endregion

    #region Full Properties


    #region ActiveGitConfiguration
    private GitConfiguration _activeGitConfiguration;
    public GitConfiguration ActiveGitConfiguration
    {
        get => _activeGitConfiguration;
        set
        {
            _activeGitConfiguration = value;
            NotifyPropertyChanged();
        }
    }
    #endregion

    #region SelectedGitConfiguration
    private GitConfiguration _selectedGitConfiguration;

    public GitConfiguration SelectedGitConfiguration
    {
        get => _selectedGitConfiguration;
        set 
        {
            _selectedGitConfiguration = value;
            _gitHelper.SetConfiguration(value, GitConfigurationScope.global);
            GitConfigurations.Insert(0, ActiveGitConfiguration);
            GitConfigurations.Remove(value);
            ActiveGitConfiguration = value;
        }
    }

    #endregion

    #region NewConfiguration
    private GitConfiguration _newConfiguration;
    public GitConfiguration NewConfiguration
    {
        get => _newConfiguration;
        set 
        {
            _newConfiguration = value;
            NotifyPropertyChanged();
        }
    }
    #endregion

    #endregion
}
