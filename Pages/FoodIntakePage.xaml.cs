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

        if(AddMealPage.CurrentAddedMeal != null)
        {
            FoodIntake.AddMeal(AddMealPage.CurrentAddedMeal);

            FoodIntake.SaveDataToPrefences();

            AddMealPage.Reset();
        }
        else if(EditMealPage.CurrentEditedMeal != null)
        {
            FoodIntake.UpdateIntakeFromMeal(EditMealPage.CurrentEditedMeal);

            FoodIntake.SaveDataToPrefences();

            EditMealPage.Reset();
        }

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

    private async void editMenuItem_Clicked(object sender, EventArgs e)
    {
        //Todo: Implement a edit function of the meal.
        //DisplayAlert("Warning", "Not Yet Implemented", "Ok");

        var menuItem = sender as MenuItem;
        var meal = menuItem.CommandParameter as Meal;

        if (meal != null)
        {
            FoodIntake.CleanMealFromDailyIntake(meal);
            await Shell.Current.GoToAsync($"{nameof(EditMealPage)}?Id={meal.Id}");
        }
    }

    private void deleteMenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var meal = menuItem.CommandParameter as Meal;

        FoodIntake.DeleteMeal(meal.Id);
        LoadData();
    }
}