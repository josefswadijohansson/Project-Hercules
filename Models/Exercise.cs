using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHercules.Models
{
    public enum ExerciseUnit { Kg, Min }

    public class Exercise
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }

        public int ExerciseValue { get; set; }

        public ExerciseUnit ExerciseUnit { get; set; }

        public string ExerciseValueAsString
        {
            get { return $"{ExerciseValue.ToString()} {ExerciseUnit.ToString()}"; }    //FIXME: Change the unit to a more dynamic 
        }
    }
}
