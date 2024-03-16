using GymFitPlus.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Core.ViewModels.ExerciseViewModels
{
    public class ExerciseAllViewModel : ExerciseForProgramViewModel
    {       
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Display(Name = "Muscle Group")]
        public MuscleGroup MuscleGroup { get; set; }

        public int UsedByProgramsCount { get; set; }
    }
}
