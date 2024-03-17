using GymFitPlus.Infrastructure.Enums;

namespace GymFitPlus.Core.ViewModels.ExerciseViewModels
{
    public class AllExercisesQueryModel
    {
        public int CurrentPage { get; set; } = 1;

        public int ExercisePerPage { get; set; } = 3;

        public MuscleGroup Category { get; set; }

        public string SearchTerm { get; set; } = string.Empty;

        public ExerciseSorting Sorting { get; set; }

        public int TotalExerciseCount { get; set; }
    }
}
