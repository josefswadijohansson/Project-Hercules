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
                return $"C={TotalCalories.ToString("0.00")}, F={TotalFat.ToString("0.00")}g, C={TotalFat.ToString("0.00")}g, P={TotalProtein.ToString("0.00")}g, S={TotalSalt.ToString("0.00")}g";
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

        public void AddComponent(MealComponent mealComponent, bool makeCopy=true)
        {
            if(makeCopy == true)
            {
                var mealC = _mealComponents.FirstOrDefault(x => x.Name == mealComponent.Name);

                if (mealC == null)
                {
                    MealComponent mealComponentCopy = MealComponent.GetCopy(mealComponent);
                    if (mealComponentCopy != null)
                    {
                        mealComponentCopy.Id = _mealComponents.Count;
                        _mealComponents.Add(mealComponentCopy);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                mealComponent.Id = _mealComponents.Count;
                _mealComponents.Add(mealComponent);
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
                return;
            }
        }

        public bool Contains(MealComponent mealComponent)
        {
            var mealC = _mealComponents.FirstOrDefault(x => x.Name == mealComponent.Name);

            if (mealC != null)
            {
                //It exist remove it from the list
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            string returnValue = $"{Name}";

            foreach(MealComponent mealComponent in _mealComponents)
            {
                returnValue += $",{mealComponent.Name}:{mealComponent.Amount}";
            }

            return returnValue;
        }
    }
}
