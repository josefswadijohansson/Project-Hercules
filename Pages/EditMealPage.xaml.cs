using ProjectHercules.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ProjectHercules.Pages;

[QueryProperty(nameof(MealId), "Id")]
public partial class EditMealPage : ContentPage
{
    private static Meal _meal;
    private static string lastChangedEntry = string.Empty;

    public static Meal CurrentEditedMeal
    {
        get { return _meal; }
    }

    public EditMealPage()
	{
		InitializeComponent();
        _meal = new Meal();
	}

    public static void Reset()
    {
        lastChangedEntry = string.Empty;
        _meal = null;
    }

    public string MealId
    {
        set
        {
            _meal = FoodIntake.GetMealById(int.Parse(value));
            if(_meal != null)
            {
                listMealComponents.ItemsSource = _meal.MealComponents;
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
    }

    public void LoadData()
    {
        if (_meal != null)
        {
            if (_meal.MealComponents.Count > 0)
            {
                var mealComponents = new ObservableCollection<MealComponent>(_meal.MealComponents);
                listMealComponents.ItemsSource = mealComponents;
            }
            else
            {
                var mealComponents = new ObservableCollection<MealComponent>();
                listMealComponents.ItemsSource = mealComponents;
            }
        }
        else
        {
            var mealComponents = new ObservableCollection<MealComponent>();
            listMealComponents.ItemsSource = mealComponents;
        }
    }


    private async void searchToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new SearchMealComponentPage(nameof(EditMealPage)));
    }

    private async void saveToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private void entryAmountMealComponent_TextChanged(object sender, TextChangedEventArgs e)
    {
        lastChangedEntry = ((((Entry)sender).Parent as StackLayout).Children[0] as Label).Text;

        if (!string.IsNullOrWhiteSpace(lastChangedEntry))
        {
            foreach (MealComponent mC in _meal.MealComponents)
            {
                if (mC.Name == lastChangedEntry)
                {
                    if (!string.IsNullOrWhiteSpace(((Entry)sender).Text))
                    {
                        try
                        {
                            mC.Amount = float.Parse(((Entry)sender).Text, CultureInfo.InvariantCulture);
                        }
                        catch { }
                    }
                    else
                    {
                        mC.Amount = 0;
                    }
                }
            }
        }
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var mealComponent = menuItem.CommandParameter as MealComponent;

        _meal.RemoveComponent(mealComponent);
        LoadData();
    }
}