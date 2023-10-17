using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHercules.Models
{
    public class MealComponent
    {
        private string _name;
        private float _amount = 0;  //in gram

        public string NutritionalFact
        {
            get
            {
                return $"C={Calories.NutrientValue}, F={Fat.NutrientValue}g, C={Carbs.NutrientValue}g, P={Protein.NutrientValue}g, S={Salt.NutrientValue}g";
            }
        }

        private Nutrient _calories = new Nutrient() { NutrientName = "Calories" };
        private Nutrient _fat = new Nutrient() { NutrientName = "Fat" };
        private Nutrient _carbs = new Nutrient() { NutrientName = "Carbs" };
        private Nutrient _protein = new Nutrient() { NutrientName = "Protein" };
        private Nutrient _salt = new Nutrient() { NutrientName = "Salt" };

        public string Name
        {
            get { return _name; }
        }
        public float Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public Nutrient Calories
        {
            get { return _calories; }
        }
        public Nutrient Fat
        {
            get { return _fat; }
        }
        public Nutrient Carbs
        {
            get { return _carbs; }
        }
        public Nutrient Protein
        {
            get { return _protein; }
        }
        public Nutrient Salt
        {
            get { return _salt; }
        }

        public float AdjustedCalories
        {
            get { return _amount * (Calories.NutrientValue / 100f); }
        }
        public float AdjustedFat
        {
            get { return _amount * (Fat.NutrientValue / 100f); }
        }
        public float AdjustedCarbs
        {
            get { return _amount * (Carbs.NutrientValue / 100f); }
        }
        public float AdjustedProtein
        {
            get { return _amount * (Protein.NutrientValue / 100f); }
        }
        public float AdjustedSalt
        {
            get { return _amount * (Salt.NutrientValue / 100f); }
        }


        public void SetValue(NutrientType type, int value)
        {
            if (type == NutrientType.Calories)
            {
                _calories.NutrientValue = value;
            }
            else if (type == NutrientType.Fat)
            {
                _fat.NutrientValue = value;
            }
            else if (type == NutrientType.Carbs)
            {
                _carbs.NutrientValue = value;
            }
            else if (type == NutrientType.Protein)
            {
                _protein.NutrientValue = value;
            }
            else if (type == NutrientType.Salt)
            {
                _salt.NutrientValue = value;
            }
        }

        public void AddValue(NutrientType type, int value)
        {
            if (type == NutrientType.Calories)
            {
                _calories.NutrientValue += value;
            }
            else if (type == NutrientType.Fat)
            {
                _fat.NutrientValue += value;
            }
            else if (type == NutrientType.Carbs)
            {
                _carbs.NutrientValue += value;
            }
            else if (type == NutrientType.Protein)
            {
                _protein.NutrientValue += value;
            }
            else if (type == NutrientType.Salt)
            {
                _salt.NutrientValue += value;
            }
        }

        public void AddValues(MealComponent mealComponent)
        {
            if(mealComponent != null)
            {
                _calories.NutrientValue += mealComponent.Calories.NutrientValue;
                _fat.NutrientValue += mealComponent.Fat.NutrientValue;
                _carbs.NutrientValue += mealComponent.Carbs.NutrientValue;
                _protein.NutrientValue += mealComponent.Protein.NutrientValue;
                _salt.NutrientValue += mealComponent.Salt.NutrientValue;
            }
        }

        public void SubractValue(NutrientType type, int value)
        {
            if (type == NutrientType.Calories)
            {
                _calories.NutrientValue -= value;
            }
            else if (type == NutrientType.Fat)
            {
                _fat.NutrientValue -= value;
            }
            else if (type == NutrientType.Carbs)
            {
                _carbs.NutrientValue -= value;
            }
            else if (type == NutrientType.Protein)
            {
                _protein.NutrientValue -= value;
            }
            else if (type == NutrientType.Salt)
            {
                _salt.NutrientValue -= value;
            }
        }

        public MealComponent(string name, float calories, float fat, float carbs, float protein, float salt)
        {
            _name = name;
            _calories.NutrientValue = calories;
            _fat.NutrientValue = fat;
            _carbs.NutrientValue = carbs;
            _protein.NutrientValue = protein;
            _salt.NutrientValue = salt;
        }
    }
}
