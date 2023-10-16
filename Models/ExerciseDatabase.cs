using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHercules.Models
{
    public static class ExerciseDatabase
    {
        public static List<Exercise> _exercises = new List<Exercise>()
        {
            new Exercise{ ExerciseId = 0, ExerciseName = "Deadlift", ExerciseValue = 10 },
            new Exercise{ ExerciseId = 1, ExerciseName = "Squat", ExerciseValue = 10 },
            new Exercise{ ExerciseId = 2, ExerciseName = "Benchpress", ExerciseValue = 10 }
        };

        public static List<Exercise> GetAllExercises() => _exercises;

        public static List<Exercise> GetWeightExercises()
        {
            List<Exercise> weightExercises = new List<Exercise>();

            foreach(Exercise e in _exercises)
            {
                if(e.ExerciseUnit == ExerciseUnit.Kg)
                {
                    weightExercises.Add(e);
                }
            }

            return weightExercises;
        }

        public static List<Exercise> GetCardioExercises()
        {
            List<Exercise> cardioExercises = new List<Exercise>();

            foreach (Exercise e in _exercises)
            {
                if (e.ExerciseUnit == ExerciseUnit.Min)
                {
                    cardioExercises.Add(e);
                }
            }

            return cardioExercises;
        }

        public static bool AddExercise(Exercise e)
        {
            e.ExerciseId = _exercises.Count - 1;
            _exercises.Add(e);

            //Todo: Add a check so the user doesnt add duplications of exercise name

            return true;
        }

        public static bool DeleteExercise(int exerciseId)
        {
            var exercise = _exercises.FirstOrDefault(x => x.ExerciseId == exerciseId);

            if (exercise != null)
            {
                _exercises.Remove(exercise);
                return true;
            }

            return false;
        }
    }
}
