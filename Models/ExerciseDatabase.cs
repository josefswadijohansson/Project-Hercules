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

        public static List<Exercise> GetExercises() => _exercises;
    }
}
