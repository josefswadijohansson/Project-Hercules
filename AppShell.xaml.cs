namespace ProjectHercules;
using ProjectHercules.Pages;
using ProjectHercules.Models;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        ExerciseRepository.LoadDataFromPreference();
        FoodDatabase.LoadData();

        Routing.RegisterRoute(nameof(OverviewPage), typeof(OverviewPage));
        Routing.RegisterRoute(nameof(FoodIntakePage), typeof(FoodIntakePage));
        Routing.RegisterRoute(nameof(FitnessPage), typeof(FitnessPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

        Routing.RegisterRoute(nameof(AddExercisePage), typeof(AddExercisePage));
        Routing.RegisterRoute(nameof(EditExercisePage), typeof(EditExercisePage));

        Routing.RegisterRoute(nameof(AddMealPage), typeof(AddMealPage));
        Routing.RegisterRoute($"{nameof(AddMealPage)}/{nameof(SearchMealComponentPage)}", typeof(SearchMealComponentPage));
    }
}
