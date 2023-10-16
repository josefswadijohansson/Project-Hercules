using ProjectHercules.Models;

namespace ProjectHercules.Pages;

[QueryProperty(nameof(ExerciseId), "Id")]
public partial class EditExercisePage : ContentPage
{
    private Exercise _exercise;
	public EditExercisePage()
	{
		InitializeComponent();
        amountUnitPicker.ItemsSource = Exercise._units;
    }

    public string ExerciseId
    {
        set
        {
            _exercise = ExerciseDatabase.GetExerciseById(int.Parse(value));
            if (_exercise != null)
            {
                entryExerciseName.Text = _exercise.ExerciseName;
                entryExerciseAmount.Text = _exercise.ExerciseValue.ToString();
                int id = (int)_exercise.ExerciseUnit;
                amountUnitPicker.SelectedIndex = id;
            }
        }
    }

    private void saveToolbarItem_Clicked(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(entryExerciseName.Text) == false &&
            string.IsNullOrWhiteSpace(entryExerciseAmount.Text) == false &&
            amountUnitPicker.SelectedIndex >= 0)
        {
            string exerciseName = entryExerciseName.Text;
            string exerciseAmount = entryExerciseAmount.Text;
            ExerciseUnit exerciseUnit = (ExerciseUnit)Enum.Parse(typeof(ExerciseUnit), Exercise._units[amountUnitPicker.SelectedIndex]);

            //Update the exercise value
            _exercise.ExerciseName = exerciseName;
            _exercise.ExerciseValue = int.Parse(exerciseAmount);
            _exercise.ExerciseUnit = exerciseUnit;

            ExerciseDatabase.SaveDataToPreference();

            Shell.Current.GoToAsync("..");
        }

        if (amountUnitPicker.SelectedIndex < 0)
        {
            DisplayAlert("Error", "Missing exercise unit", "Ok");
        }

        if (string.IsNullOrWhiteSpace(entryExerciseName.Text) == true)
        {
            DisplayAlert("Error", "Missing exercise name", "Ok");
        }

        if (string.IsNullOrWhiteSpace(entryExerciseAmount.Text) == true)
        {
            DisplayAlert("Error", "Missing exercise amount", "Ok");
        }
    }
}