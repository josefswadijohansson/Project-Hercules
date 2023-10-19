using ProjectHercules.Models;

namespace ProjectHercules.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }



    public static void ResetSavedData()
    {
        Preferences.Remove(FoodIntake.SavedDataKey);
        Preferences.Remove(ExerciseRepository.SavedDataKey);

        FoodIntake.Clean();
        ExerciseRepository.Clean();
    }

    private async void removeAllDataButton_Clicked(object sender, EventArgs e)
    {
        string message = "Do you want to remove all the saved data from this device?@By agreeing you accept that all the data will be removed from this device";
        message = message.Replace("@", System.Environment.NewLine + System.Environment.NewLine);
        bool answer = await DisplayAlert("Warning!", message, "Yes", "No");

        if(answer == true)
        {
            ResetSavedData();
        }
    }
}