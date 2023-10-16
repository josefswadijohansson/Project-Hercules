namespace ProjectHercules;
using ProjectHercules.Pages;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(OverviewPage), typeof(OverviewPage));
	}
}
