using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.FitnessProgramConstants;

namespace GymFitPlus.Core.ViewModels.FitnessProgramViewModels
{
    public class FitnessProgramExercisesInfoViewModel
    {
        public int FitnessProgramId { get; set; }

        public int ExerciseId { get; set; }

        public string ExerciseName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(ExercisePropertiesMinValue, 
               ExercisePropertiesMaxValue,
               ErrorMessage = ExercisePropertiesErrorMessages)]
        public int Reps { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(ExercisePropertiesMinValue,
               ExercisePropertiesMaxValue,
               ErrorMessage = ExercisePropertiesErrorMessages)]
        public int Sets { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(ExercisePropertiesMinValue,
               ExercisePropertiesMaxValue, 
               ErrorMessage = ExercisePropertiesErrorMessages)]
        public double Weight { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(ExercisePropertiesMinValue,
               ExercisePropertiesMaxValue,
               ErrorMessage = ExercisePropertiesErrorMessages)]
        public int Order { get; set; }

        public IEnumerable<ExerciseForProgramViewModel> Exercises { get; set; } = new List<ExerciseForProgramViewModel>();
    }
}
