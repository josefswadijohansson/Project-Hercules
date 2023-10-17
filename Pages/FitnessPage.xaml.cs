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
        ObservableCollection<Exercise> weightExercises = new ObservableCollection<Exercise>(ExerciseRepository.GetWeightExercises());
        ObservableCollection<Exercise> cardioExercises = new ObservableCollection<Exercise>(ExerciseRepository.GetCardioExercises());

        listWeights.ItemsSource = weightExercises;
        listCardio.ItemsSource = cardioExercises;
    }

    private void listExercises_ItemTapped(object sender, ItemTappedEventArgs e)
    {

    }

    private void listCardio_ItemTapped(object sender, ItemTappedEventArgs e)
    {

    }

    private async void addToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new AddExercisePage());   
    }

    private void deleteMenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var exercise = menuItem.CommandParameter as Exercise;

        ExerciseRepository.DeleteExercise(exercise.ExerciseId);

        LoadData();
    }

    private async void editMenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var exercise = menuItem.CommandParameter as Exercise;

        if(exercise != null)
        {
            await Shell.Current.GoToAsync($"{nameof(EditExercisePage)}?Id={exercise.ExerciseId}");
        }
    }
}