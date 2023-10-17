namespace ProjectHercules.Pages;

using System.Collections.ObjectModel;
using ProjectHercules.Models;

public partial class OverviewPage : ContentPage
{
    public OverviewPage()
	{
		InitializeComponent();

        LoadContent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadContent();
    }

    private void btnAddCalories_Clicked(object sender, EventArgs e)
    {
        //listExercises.ItemsSource = intakes;
    }

    public void LoadContent()
    {
        ObservableCollection<Nutrient> nutrientIntake = new ObservableCollection<Nutrient>(FoodIntake.GetNutrients());
        ObservableCollection<Exercise> exercises = new ObservableCollection<Exercise>(ExerciseRepository.GetAllExercises());

        listNutrients.ItemsSource = nutrientIntake;
        listExercises.ItemsSource = exercises;
    }
}