using ProjectHercules.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace ProjectHercules.Pages;

public partial class AddMealPage : ContentPage
{

    public static Meal Meal { get; set; }

    private static string lastChangedEntry = string.Empty;
    private Dictionary<string, string> mealEntrys = new Dictionary<string, string>();

	public AddMealPage()
	{
		InitializeComponent();
        Reset();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
    }

    private async void saveToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        //Todo: Implement a delete function of the meal component.
        DisplayAlert("Warning", "Not Yet Implemented", "Ok");
    }

    private async void searchToolbarItem_Clicked(object sender, EventArgs e)
    {
        //Shell.Current.Navigation.PushAsync(new SearchMealComponentPage());
        await Shell.Current.Navigation.PushAsync(new SearchMealComponentPage());
    }

    public static void Reset()
    {
        lastChangedEntry = string.Empty;
        if (Meal == null)
        {
            Meal = new Meal();
        }
    }

    public void LoadData()
    {
        if(Meal != null)
        {
            if(Meal.MealComponents.Count > 0)
            {
                var mealComponents = new ObservableCollection<MealComponent>(Meal.MealComponents);
                listMealComponents.ItemsSource = mealComponents;
            }
        }
    }

    private void entryAmountMealComponent_TextChanged(object sender, TextChangedEventArgs e)
    {
        lastChangedEntry = ((((Entry)sender).Parent as StackLayout).Children[0] as Label).Text;

        if(!string.IsNullOrWhiteSpace(lastChangedEntry))
        {
            foreach (MealComponent mC in Meal.MealComponents)
            {
                if (mC.Name == lastChangedEntry)
                {
                    if(!string.IsNullOrWhiteSpace(((Entry)sender).Text))
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
}