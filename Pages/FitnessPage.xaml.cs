using System.Collections.ObjectModel;
using ProjectHercules.Models;

namespace ProjectHercules.Pages;

public partial class FitnessPage : ContentPage
{

	public FitnessPage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        LoadData();
    }

    public void LoadData()
    {
        ObservableCollection<Exercise> weightExercises = new ObservableCollection<Exercise>(ExerciseDatabase.GetWeightExercises());
        ObservableCollection<Exercise> cardioExercises = new ObservableCollection<Exercise>(ExerciseDatabase.GetCardioExercises());

        listWeights.ItemsSource = weightExercises;
        listCardio.ItemsSource = cardioExercises;
    }

    private void listExercises_ItemTapped(object sender, ItemTappedEventArgs e)
    {

    }

    private void listCardio_ItemTapped(object sender, ItemTappedEventArgs e)
    {

    }

    private void addToolbarItem_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddExercisePage));   
    }

    private void deleteMenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var exercise = menuItem.CommandParameter as Exercise;

        ExerciseDatabase.DeleteExercise(exercise.ExerciseId);

        LoadData();
    }
}