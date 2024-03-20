using System.Collections.ObjectModel;
using System.Diagnostics;
using ProjectHercules.Models;

namespace ProjectHercules.Pages;

public partial class SearchMealComponentPage : ContentPage
{
    private string _currentQuery = string.Empty;
    private int _wantedSize = 10;
    private int _currentWantedSize = 10;

    private int _pageIndex = 0;

    private List<MealComponent> _filteredMealComponents = new List<MealComponent>();

	public SearchMealComponentPage(string parentPage)
	{
		InitializeComponent();
        ParentPage = parentPage;
        _currentWantedSize = _wantedSize;
        //LoadData();
    }

    public string ParentPage = string.Empty;

    protected override void OnAppearing()
    {
        base.OnAppearing();

        previousButton.IsEnabled = false;
        nextButton.IsEnabled = false;

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
            _pageIndex = 0;
            pageCountLabel.Text = _pageIndex.ToString();

            _filteredMealComponents = new List<MealComponent>(FoodDatabase.GetMealComponents(((SearchBar)sender).Text));

            List<MealComponent> tempComponents = new List<MealComponent>(_filteredMealComponents);

            bool isBiggerThanWantedSize = _filteredMealComponents.Count > _currentWantedSize;

            if(isBiggerThanWantedSize)
            {
                tempComponents.RemoveRange(_currentWantedSize, tempComponents.Count - _currentWantedSize);

                //TODO: Showcase either [1,2,3,...] or a load more
                nextButton.IsEnabled = true;
            }

            var mealComponents = new ObservableCollection<MealComponent>(tempComponents);
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

    private void searchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        searchBar.Unfocus();
    }

    private void LoadItems(int newPageIndex)
    {
        List<MealComponent> tempComponents = new List<MealComponent>(_filteredMealComponents);

        int startIndex = GetStartIndex(newPageIndex, _wantedSize);

        var mealComponents = new ObservableCollection<MealComponent>(CleanUpList(tempComponents, startIndex, _wantedSize));
        listMealComponents.ItemsSource = mealComponents;
    }

    private void previousButton_Clicked(object sender, EventArgs e)
    {
        _pageIndex--;

        if (_pageIndex > 0)
        {
            previousButton.IsEnabled = true;
            nextButton.IsEnabled = true;
        }
        else if (_pageIndex <= 0)
        {
            previousButton.IsEnabled = false;
            nextButton.IsEnabled = true;
        }

        LoadItems(_pageIndex);

        pageCountLabel.Text = _pageIndex.ToString();
    }

    private void nextButton_Clicked(object sender, EventArgs e)
    {
        _pageIndex++;

        int totalElements = _filteredMealComponents.Count;
        int elementsPerPage = _wantedSize;

        double howManyPagesInTotal = Math.Ceiling((float)_filteredMealComponents.Count / (float)_wantedSize) - 1;

        if (_pageIndex < howManyPagesInTotal) // 1 < 2 == false
        {
            previousButton.IsEnabled = true;
            nextButton.IsEnabled = true;
        }
        else
        {
            previousButton.IsEnabled = true;
            nextButton.IsEnabled = false;
        }

        LoadItems(_pageIndex);

        pageCountLabel.Text = _pageIndex.ToString();
    }

    private int GetStartIndex(int pageIndex, int size)
    {
        return pageIndex * size;
    }

    private List<MealComponent> CleanUpList(List<MealComponent> mealComponents, int startIndex, int count)
    {
        List<MealComponent> tempComponents = new List<MealComponent>();

        int currentLeft = count;

        for(int i = startIndex; i < mealComponents.Count; i++)
        {
            tempComponents.Add(mealComponents[i]);
            currentLeft--;

            if(currentLeft <= 0)
            {
                break;
            }
        }

        return tempComponents;
    }

    private void listMealComponents_Scrolled(object sender, ScrolledEventArgs e)
    {
        //searchBar.Unfocus();
    }
}