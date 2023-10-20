namespace ProjectHercules;

using ProjectHercules.Models;
using ProjectHercules.Pages;
using System.Diagnostics;

public partial class App : Application
{
    public static App Instance;
    public static int CurrentPageID;
	public App()
	{
		InitializeComponent();
        Instance = this;
        FoodDatabase.LoadData();

        MainPage = new AppShell();
    }

    protected override void OnResume()
    {
        //Todo maybe add so the app load all the data on new, to ensure the user have the saved data always when the app reopens.
        base.OnResume();
    }
}
