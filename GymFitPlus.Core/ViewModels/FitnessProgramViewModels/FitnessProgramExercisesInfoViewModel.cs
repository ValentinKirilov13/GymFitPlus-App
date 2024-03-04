using GymFitPlus.Core.ViewModels.ExerciseViewModels;

namespace GymFitPlus.Core.ViewModels.FitnessProgramViewModels
{
    public class FitnessProgramExercisesInfoViewModel
    {
        public int FitnessProgramId { get; set; }
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public int Reps { get; set; }
        public int Sets { get; set; }
        public int Weight { get; set; }

        public List<ExerciseForProgramViewModel> Exercises { get; set; } = new List<ExerciseForProgramViewModel>();
    }
}
