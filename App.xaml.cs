namespace ProjectHercules;
using ProjectHercules.Pages;
public partial class App : Application
{
    public static App Instance;
    public static int CurrentPageID;
	public App()
	{
		InitializeComponent();
        Instance = this;
        MainPage = new AppShell();
    }
}
