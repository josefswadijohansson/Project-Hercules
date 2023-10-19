namespace ProjectHercules.Pages;
using ProjectHercules.Models;

public partial class AddExercisePage : ContentPage
{
    

    public AddExercisePage()
	{
		InitializeComponent();
        
        amountUnitPicker.ItemsSource = Exercise._units;
	}

    private async void saveToolbarItem_Clicked(object sender, EventArgs e)
    {
        if(string.IsNullOrWhiteSpace(entryExerciseName.Text) == false &&
            string.IsNullOrWhiteSpace(entryExerciseAmount.Text) == false &&
            amountUnitPicker.SelectedIndex >= 0)
        {
            string exerciseName = entryExerciseName.Text;
            string exerciseAmount = entryExerciseAmount.Text;
            ExerciseUnit exerciseUnit = (ExerciseUnit)Enum.Parse(typeof(ExerciseUnit), Exercise._units[amountUnitPicker.SelectedIndex]);

            Exercise newExercise = new Exercise() { ExerciseName = exerciseName, ExerciseValue = int.Parse(exerciseAmount), ExerciseUnit = exerciseUnit };

            ExerciseRepository.AddExercise(newExercise);

            ExerciseRepository.SaveDataToPreference();

            await Shell.Current.Navigation.PopAsync();
        }

        if(amountUnitPicker.SelectedIndex < 0)
        {
            await DisplayAlert("Error", "Missing exercise unit", "Ok");
        }

        if(string.IsNullOrWhiteSpace(entryExerciseName.Text) == true)
        {
            await DisplayAlert("Error", "Missing exercise name", "Ok");
        }

        if(string.IsNullOrWhiteSpace(entryExerciseAmount.Text) == true)
        {
            await DisplayAlert("Error", "Missing exercise amount", "Ok");
        }
    }
}