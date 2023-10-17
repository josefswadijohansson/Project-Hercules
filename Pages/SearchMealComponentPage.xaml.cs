using System.Collections.ObjectModel;
using ProjectHercules.Models;

namespace ProjectHercules.Pages;

public partial class SearchMealComponentPage : ContentPage
{
	public SearchMealComponentPage()
	{
		InitializeComponent();
        //LoadData();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        //LoadData();
    }
    private void LoadData()
    {
        var mealComponents = new ObservableCollection<MealComponent>(FoodDatabase.GetAllMealComponents());
        listMealComponents.ItemsSource = mealComponents;
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(((SearchBar)sender).Text))
        {
            var mealComponents = new ObservableCollection<MealComponent>(FoodDatabase.GetMealComponents(((SearchBar)sender).Text));
            listMealComponents.ItemsSource = mealComponents;
        }
        else
        {
            var mealComponents = new ObservableCollection<MealComponent>();
            listMealComponents.ItemsSource = mealComponents;
        }
    }

    private async void listMealComponents_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        MealComponent m = listMealComponents.SelectedItem as MealComponent;
        AddMealPage.Meal.AddComponent(m);

        listMealComponents.SelectedItem = null;

        await Shell.Current.Navigation.PopAsync();
    }

    private void listMealComponents_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        /*MealComponent m = listMealComponents.SelectedItem as MealComponent;
        AddMealPage.Meal.AddComponent(m);

        await Shell.Current.Navigation.PopAsync();
        ///{nameof(Page3)}/{nameof(StatsPage)}
        */
    }
}