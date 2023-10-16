namespace ProjectHercules.Pages;
using ProjectHercules.Models;

public partial class AddExercisePage : ContentPage
{
    

    public AddExercisePage()
	{
		InitializeComponent();
        
        amountUnitPicker.ItemsSource = Exercise._units;
	}

    private void saveToolbarItem_Clicked(object sender, EventArgs e)
    {
        //Todo : Add so it add the exercise down
        //Todo : Add a check so it doesnt save an empty Exercise
        
        if(string.IsNullOrWhiteSpace(entryExerciseName.Text) == false &&
            string.IsNullOrWhiteSpace(entryExerciseAmount.Text) == false &&
            amountUnitPicker.SelectedIndex >= 0)
        {
            string exerciseName = entryExerciseName.Text;
            string exerciseAmount = entryExerciseAmount.Text;
            ExerciseUnit exerciseUnit = (ExerciseUnit)Enum.Parse(typeof(ExerciseUnit), Exercise._units[amountUnitPicker.SelectedIndex]);

            Exercise newExercise = new Exercise() { ExerciseName = exerciseName, ExerciseValue = int.Parse(exerciseAmount), ExerciseUnit = exerciseUnit };

            ExerciseDatabase.AddExercise(newExercise);

            ExerciseDatabase.SaveDataToPreference();

            Shell.Current.GoToAsync("..");
        }

        if(amountUnitPicker.SelectedIndex < 0)
        {
            DisplayAlert("Error", "Missing exercise unit", "Ok");
        }

        if(string.IsNullOrWhiteSpace(entryExerciseName.Text) == true)
        {
            DisplayAlert("Error", "Missing exercise name", "Ok");
        }

        if(string.IsNullOrWhiteSpace(entryExerciseAmount.Text) == true)
        {
            DisplayAlert("Error", "Missing exercise amount", "Ok");
        }
    }
}