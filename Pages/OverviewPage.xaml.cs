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
        ExerciseDatabase.LoadDataFromPreference();

        ObservableCollection<Nutrient> foodIntake = new ObservableCollection<Nutrient>(FoodIntake.GetNutrients());
        ObservableCollection<Exercise> exercises = new ObservableCollection<Exercise>(ExerciseDatabase.GetAllExercises());

        listNutrients.ItemsSource = foodIntake;
        listExercises.ItemsSource = exercises;
    }

    private void listNutrients_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Nutrient n = listNutrients.SelectedItem as Nutrient;
        n.NutrientValue++;
        LoadContent();
    }

    private void listExercises_ItemTapped(object sender, ItemTappedEventArgs e)
    {

    }
}