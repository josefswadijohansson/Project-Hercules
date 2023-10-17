using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectHercules.Pages;

namespace ProjectHercules.Models
{
    public static class FoodIntake
    {
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

        public static void SetValue(NutrientType type, float value)
        {
            GetNutrient(type).NutrientValue = value;
        }

        public static void AddValue(NutrientType type, float value)
        {
            GetNutrient(type).NutrientValue += value;
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

                if(Meals.Count > 1)
                {
                    meal.Id = Meals.Count - 1;
                }
                else if(Meals.Count == 0)
                {
                    meal.Id = 0;
                }

                Meals.Add(meal);
                meal.Name = DateTime.Now.ToString("HH:mm");
                AddMealPage.Meal = null;
            }
        }
    }
}
