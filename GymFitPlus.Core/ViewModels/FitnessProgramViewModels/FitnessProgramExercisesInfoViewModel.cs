using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Core.ViewModels.FitnessProgramViewModels
{
    public class FitnessProgramExercisesInfoViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int FitnessProgramId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(0,100,ErrorMessage = "not in range")]
        public int Reps { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(0, 100,ErrorMessage = "not in range")]
        public int Sets { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(0, 100, ErrorMessage = "not in range")]
        public int Weight { get; set; }

        public IEnumerable<ExerciseForProgramViewModel> Exercises { get; set; } = new List<ExerciseForProgramViewModel>();


        public int Order { get; set; }
    }
}
