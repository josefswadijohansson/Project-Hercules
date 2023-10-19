using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectHercules.Pages;

namespace ProjectHercules.Models
{
    public static class FoodIntake
    {
        public static readonly string SavedDataKey = "dailyFoodIntake_savedValue";

        public static List<Nutrient> Nutrients = new List<Nutrient>()
        {
            new Nutrient { NutrientId = 0, NutrientName = "Calories", NutrientValue = 0 },
            new Nutrient { NutrientId = 1, NutrientName = "Fat", NutrientValue = 0 },
            new Nutrient { NutrientId = 2, NutrientName = "Carbs", NutrientValue = 0 },
            new Nutrient { NutrientId = 3, NutrientName = "Protein", NutrientValue = 0 },
            new Nutrient { NutrientId = 4, NutrientName = "Salt", NutrientValue = 0 }
        };
        public static List<Meal> Meals = new List<Meal>();
        
        public static List<Nutrient> GetNutrients() => Nutrients;
        public static List<Meal> GetMeals() => Meals;

        public static Nutrient GetNutrient(NutrientType type)
        {
            foreach(Nutrient n in Nutrients)
            {
                if(n.NutrientName == type.ToString())
                {
                    return n;
                }
            }

            return null;
        }

        public static Meal GetMealById(int mealId)
        {
            var meal = Meals.FirstOrDefault(x => x.Id == mealId);

            if (meal != null)
            {
                return meal;
            }

            return null;
        }

        public static void SetValue(NutrientType type, float value)
        {
            GetNutrient(type).NutrientValue = value;
        }

        public static void AddValue(NutrientType type, float value)
        {
            GetNutrient(type).NutrientValue += value;
        }
        public static void RemoveValue(NutrientType type, float value)
        {
            GetNutrient(type).NutrientValue -= value;
        }

        public static void AddMeal(Meal meal)
        {
            if(meal != null)
            {
                Meal mealCheck = Meals.FirstOrDefault(x => x.Id == meal.Id);

                if (mealCheck != null)
                {
                    //There is already an meal with that id
                    return;
                }

                if(meal.MealComponents.Count == 0)
                {
                    return;
                }

                AddValue(NutrientType.Calories, meal.TotalCalories);
                AddValue(NutrientType.Fat, meal.TotalFat);
                AddValue(NutrientType.Carbs, meal.TotalCarbs);
                AddValue(NutrientType.Protein, meal.TotalProtein);
                AddValue(NutrientType.Salt, meal.TotalSalt);

                meal.Id = Meals.Count;

                Meals.Add(meal);
                meal.Name = DateTime.Now.ToString("HH:mm:ss");
            }
        }

        public static void CleanMealFromDailyIntake(Meal meal)
        {
            if (meal != null)
            {
                Meal mealToClean = Meals.FirstOrDefault(x => x.Id == meal.Id);

                if (mealToClean != null)
                {
                    RemoveValue(NutrientType.Calories, mealToClean.TotalCalories);
                    RemoveValue(NutrientType.Fat, mealToClean.TotalFat);
                    RemoveValue(NutrientType.Carbs, mealToClean.TotalCarbs);
                    RemoveValue(NutrientType.Protein, mealToClean.TotalProtein);
                    RemoveValue(NutrientType.Salt, mealToClean.TotalSalt);
                    return;
                }

                if (meal.MealComponents.Count == 0)
                {
                    return;
                }
            }
        }

        public static void UpdateIntakeFromMeal(Meal meal)
        {
            if (meal != null)
            {
                Meal mealToUpdate = Meals.FirstOrDefault(x => x.Id == meal.Id);

                if (mealToUpdate != null)
                {
                    if (mealToUpdate.MealComponents.Count == 0)
                    {
                        return;
                    }

                    AddValue(NutrientType.Calories, mealToUpdate.TotalCalories);
                    AddValue(NutrientType.Fat, mealToUpdate.TotalFat);
                    AddValue(NutrientType.Carbs, mealToUpdate.TotalCarbs);
                    AddValue(NutrientType.Protein, mealToUpdate.TotalProtein);
                    AddValue(NutrientType.Salt, mealToUpdate.TotalSalt);
                    return;
                }

                
            }
        }

        public static bool DeleteMeal(int mealId)
        {
            Meal mealToDelete = Meals.FirstOrDefault(x => x.Id == mealId);

            if (mealToDelete != null)
            {
                RemoveValue(NutrientType.Calories, mealToDelete.TotalCalories);
                RemoveValue(NutrientType.Fat, mealToDelete.TotalFat);
                RemoveValue(NutrientType.Carbs, mealToDelete.TotalCarbs);
                RemoveValue(NutrientType.Protein, mealToDelete.TotalProtein);
                RemoveValue(NutrientType.Salt, mealToDelete.TotalSalt);

                Meals.Remove(mealToDelete);

                SaveDataToPrefences();
            }
            return false;
        }

        public static void LoadDataFromPrefences()
        {
            string dataToLoad = Preferences.Get("dailyFoodIntake_savedValue", "defaultValue");

            if (!dataToLoad.Equals("defaultValue"))
            {
                string[] dataValues = dataToLoad.Split('|');

                if(dataValues.Length > 0)   //the loaded data values array is not empty
                {
                    string date = dataValues[0];
                    string todayAsString = DateTime.Now.ToString("yy-MM-dd");
                    //If the date from the saved time is not today, then cancel this function, and reset the preference value to a default value
                    if(date != todayAsString)
                    {
                        //Reset the preference value to empty;
                        string resetValue = $"{DateTime.Now.ToString("yy-MM-dd")}|";

                        Preferences.Set("dailyFoodIntake_savedValue", resetValue);
                        return;
                    }

                    Meal meal = new Meal();
                    
                    for(int i = 1; i < dataValues.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(dataValues[i]) || string.IsNullOrEmpty(dataValues[i]))
                        {
                            continue;
                        }

                        string[] meals = dataValues[i].Split(';');

                        if(meals.Length > 0)    //there are meals in the saved data
                        {
                            foreach(string mealAsString in meals)
                            {
                                if (string.IsNullOrWhiteSpace(mealAsString) || string.IsNullOrEmpty(mealAsString))
                                {
                                    continue;
                                }

                                string[] mealValues = mealAsString.Split(',');

                                if (mealValues.Length > 0)
                                {
                                    string mealName = mealValues[0];
                                    meal.Name = mealName;

                                    for (int j = 1; j < mealValues.Length; j++)
                                    {
                                        string[] mealComponentValues = mealValues[j].Split(':');

                                        if(mealComponentValues.Length > 0)
                                        {
                                            MealComponent mealComponent = MealComponent.GetCopy(FoodDatabase.GetMealComponent(mealComponentValues[0]));

                                            mealComponent.Amount = float.Parse(mealComponentValues[1], CultureInfo.InvariantCulture);

                                            meal.AddComponent(mealComponent, false);
                                        }
                                    }
                                }
                            }

                        }
                    }

                    AddMeal(meal);
                }
            }
        }

        public static string GetDailyIntakeAsString()
        {
            string value = string.Empty;

            //Take all the meals and squish it down to a string, the only thing the system needs is : what meal component(the name), and the amount the person ate.
            //Also put in the beginning of the string the date, so we know if the meals is from today, if not reset the meals and nutrients the user has eaten.

            value += $"{DateTime.Now.ToString("yy-MM-dd")}|";

            foreach (Meal meal in Meals)
            {
                value += $"{meal.ToString()};";
            }

            return value;
        }

        public static void SaveDataToPrefences()
        {
            string dataToSave = GetDailyIntakeAsString();
            Preferences.Set("dailyFoodIntake_savedValue", dataToSave);
        }

        public static void Clean()
        {
            Nutrients = new List<Nutrient>()
            {
                new Nutrient { NutrientId = 0, NutrientName = "Calories", NutrientValue = 0 },
                new Nutrient { NutrientId = 1, NutrientName = "Fat", NutrientValue = 0 },
                new Nutrient { NutrientId = 2, NutrientName = "Carbs", NutrientValue = 0 },
                new Nutrient { NutrientId = 3, NutrientName = "Protein", NutrientValue = 0 },
                new Nutrient { NutrientId = 4, NutrientName = "Salt", NutrientValue = 0 }
            };

            Meals = new List<Meal>();
        }
    }
}
