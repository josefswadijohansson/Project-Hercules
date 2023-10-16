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

        public static List<string> _units = new List<string>() { ExerciseUnit.Kg.ToString(), ExerciseUnit.Min.ToString() };

        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }

        public int ExerciseValue { get; set; }

        public ExerciseUnit ExerciseUnit { get; set; }

        public string ExerciseValueAsString
        {
            get { return $"{ExerciseValue.ToString()} {ExerciseUnit.ToString()}"; }    //FIXME: Change the unit to a more dynamic 
        }

        public override string ToString()
        {
            string value = $"{ExerciseName},{ExerciseValue},{ExerciseUnit.ToString()}";
            return value;
        }

        public static Exercise CreateFromData(string loadedData)
        {
            //Deadlift,20,kg
            string[] dataToLoadIn = loadedData.Split(",");
            Exercise exercise = new Exercise()
            {
                ExerciseName = dataToLoadIn[0],
                ExerciseValue = int.Parse(dataToLoadIn[1]),
                ExerciseUnit = (ExerciseUnit) Enum.Parse(typeof(ExerciseUnit), dataToLoadIn[2])
            };

            return exercise;
        }
    }
}
