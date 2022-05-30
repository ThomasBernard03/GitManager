
using GitManager.ViewModels;

namespace GitManager.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		BindingContext = App.serviceProvider.GetService(typeof(HomeViewModel));
	}
}