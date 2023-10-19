using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHercules.Models
{
    public static class ExerciseRepository
    {
        public static readonly string SavedDataKey = "exercises_savedValue";

        public static List<Exercise> Exercises = new List<Exercise>()
        {
            /*new Exercise{ ExerciseId = 0, ExerciseName = "Deadlift", ExerciseValue = 10 },
            new Exercise{ ExerciseId = 1, ExerciseName = "Squat", ExerciseValue = 10 },
            new Exercise{ ExerciseId = 2, ExerciseName = "Benchpress", ExerciseValue = 10 }*/
        };

        public static List<Exercise> GetAllExercises() => Exercises;

        public static Exercise GetExerciseById(int exerciseId)
        {
            var exercise = Exercises.FirstOrDefault(x => x.ExerciseId == exerciseId);

            if (exercise != null)
            {
                
                return exercise;
            }

            return null;
        }

        public static List<Exercise> GetWeightExercises()
        {
            List<Exercise> weightExercises = new List<Exercise>();

            foreach(Exercise e in Exercises)
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

            foreach (Exercise e in Exercises)
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
            e.ExerciseId = Exercises.Count - 1;

            var exercise = Exercises.FirstOrDefault(x => x.ExerciseName == e.ExerciseName);

            if (exercise == null)
            {
                Exercises.Add(e);
            }
            else
            {
                return false;   
            }

            return true;
        }

        public static bool DeleteExercise(int exerciseId)
        {
            var exercise = Exercises.FirstOrDefault(x => x.ExerciseId == exerciseId);

            if (exercise != null)
            {
                Exercises.Remove(exercise);
                return true;
            }

            return false;
        }

        public static void LoadDataFromPreference()
        {
            string dataToLoad = Preferences.Get("exercises_savedValue", "defaultValue");

            if (!dataToLoad.Equals("defaultValue"))
            {
                string[] exercisesArray = dataToLoad.Split(';');

                if (exercisesArray.Length > 0)
                {
                    foreach(string exerciseData in exercisesArray)
                    {
                        if(!string.IsNullOrWhiteSpace(exerciseData))
                        {
                            Exercise e = Exercise.CreateFromData(exerciseData);
                            if(ExerciseRepository.Exercises.Count > 1)
                            {
                                e.ExerciseId = ExerciseRepository.Exercises.Count - 1;
                            }
                            ExerciseRepository.AddExercise(e);
                        }
                    }
                }
            }

        }

        public static void SaveDataToPreference()
        {
            string dataToSave = string.Empty;

            foreach(Exercise e in Exercises)
            {
                dataToSave += $"{e.ToString()};";
            }

            Preferences.Set("exercises_savedValue", dataToSave);
        }

        public static void Clean()
        {
            Exercises = new List<Exercise>();
        }
    }
}
