using System.Collections.ObjectModel;
using ProjectHercules.Models;

namespace ProjectHercules.Pages;

public partial class SearchMealComponentPage : ContentPage
{
	public SearchMealComponentPage(string parentPage)
	{
		InitializeComponent();
        ParentPage = parentPage;
        //LoadData();
    }

    public string ParentPage = string.Empty;

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

        if(ParentPage == nameof(AddMealPage))
        {
            if(AddMealPage.CurrentAddedMeal != null)
            {
                AddMealPage.CurrentAddedMeal.AddComponent(m);
            }
            else if(AddMealPage.CurrentAddedMeal == null)
            {
                await DisplayAlert("Error", "Why is current add meal null?", "OK");
            }
        }
        else
        {
            if(EditMealPage.CurrentEditedMeal != null)
            {
                EditMealPage.CurrentEditedMeal.AddComponent(m);
            }
            else if(EditMealPage.CurrentEditedMeal == null)
            {
                await DisplayAlert("Error", "Why is current edit meal null?", "OK");
            }
        }

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