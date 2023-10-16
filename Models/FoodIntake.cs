using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHercules.Models
{
    

    public static class FoodIntake
    {
        public static List<Nutrient> _nutrients = new List<Nutrient>()
        {
            new Nutrient { NutrientId = 0, NutrientName = "Calories", NutrientValue = 55 },
            new Nutrient { NutrientId = 1, NutrientName = "Fat", NutrientValue = 55 },
            new Nutrient { NutrientId = 1, NutrientName = "Carbs", NutrientValue = 55 },
            new Nutrient { NutrientId = 1, NutrientName = "Protein", NutrientValue = 55 },
            new Nutrient { NutrientId = 1, NutrientName = "Salt", NutrientValue = 55 }
        };
        
        public static List<Nutrient> GetNutrients() => _nutrients;
    }
}
