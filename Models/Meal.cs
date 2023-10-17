using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHercules.Models
{
    public class Meal
    {
        public int Id = -1;
        public string Name { get; set; }
        public string NutritionalFact 
        { 
            get
            {
                return $"C={TotalCalories}, F={TotalFat}g, C={TotalFat}g, P={TotalProtein}g, S={TotalSalt}g";
            }
        }

        private List<MealComponent> _mealComponents = new List<MealComponent>();

        public List<MealComponent> MealComponents
        { 
            get { return _mealComponents; } 
        }

        public float TotalCalories
        {
            get 
            {
                float totalCalories = 0;
                foreach (MealComponent component in _mealComponents)
                {
                    totalCalories += component.AdjustedCalories;
                }
                return totalCalories; 
            }
        }
        public float TotalFat
        {
            get 
            {
                float totalFat = 0;
                foreach (MealComponent component in _mealComponents)
                {
                    totalFat += component.AdjustedFat;
                }
                return totalFat;
            }
        }
        public float TotalCarbs
        {
            get
            {
                float totalCarbs = 0;
                foreach (MealComponent component in _mealComponents)
                {
                    totalCarbs += component.AdjustedCarbs;
                }
                return totalCarbs;
            }
        }
        public float TotalProtein
        {
            get
            {
                float totalProtein = 0;
                foreach (MealComponent component in _mealComponents)
                {
                    totalProtein += component.AdjustedProtein;
                }
                return totalProtein;
            }
        }
        public float TotalSalt
        {
            get
            {
                float totalSalt = 0;
                foreach (MealComponent component in _mealComponents)
                {
                    totalSalt += component.AdjustedSalt;
                }
                return totalSalt;
            }
        }

        public void AddComponent(MealComponent mealComponent)
        {
            var mealC = _mealComponents.FirstOrDefault(x => x.Name == mealComponent.Name);

            if (mealC == null)
            {
                _mealComponents.Add(mealComponent);
            }
            else
            {
                //It already exist in the list, maybe just add the nutrient values to the existing meal component.
                mealC.AddValues(mealComponent);
                return;
            }
        }

        public void RemoveComponent(MealComponent mealComponent)
        {
            var mealC = _mealComponents.FirstOrDefault(x => x.Name == mealComponent.Name);

            if (mealC != null)
            {
                //It exist remove it from the list
                _mealComponents.Remove(mealC);
            }
            else
            {
                // It doesnt exist in the list
                return;
            }
        }
    }
}
