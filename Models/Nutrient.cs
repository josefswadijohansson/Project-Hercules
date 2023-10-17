using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHercules.Models
{
    public enum NutrientType { Calories, Fat, Carbs, Protein, Salt }

    public class Nutrient
    {
        public int NutrientId { get; set; }
        public string NutrientName { get; set; }

        public float NutrientValue { get; set; }
    }
}
