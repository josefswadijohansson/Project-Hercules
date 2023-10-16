namespace ProjectHercules;
using ProjectHercules.Pages;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(OverviewPage), typeof(OverviewPage));
        Routing.RegisterRoute(nameof(FoodIntakePage), typeof(FoodIntakePage));
        Routing.RegisterRoute(nameof(FitnessPage), typeof(FitnessPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}
