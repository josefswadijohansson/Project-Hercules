using ProjectHercules.Models;
using System.Collections.ObjectModel;

namespace ProjectHercules.Pages;

public partial class FoodIntakePage : ContentPage
{
	public FoodIntakePage()
	{
		InitializeComponent();
        LoadData();
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        FoodIntake.AddMeal(AddMealPage.Meal);
        AddMealPage.Reset();

        LoadData();
    }

    public void LoadData()
    {
        ObservableCollection<Meal> mealsExercises = new ObservableCollection<Meal>(FoodIntake.GetMeals());

        listMeals.ItemsSource = mealsExercises;
    }

    private async void addToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new AddMealPage());
    }

    private void editMenuItem_Clicked(object sender, EventArgs e)
    {
        //Todo: Implement a edit function of the meal.
        DisplayAlert("Warning", "Not Yet Implemented", "Ok");
    }

    private void deleteMenuItem_Clicked(object sender, EventArgs e)
    {
        //Todo: Implement a delete function of the meal.
        DisplayAlert("Warning", "Not Yet Implemented", "Ok");
    }
}