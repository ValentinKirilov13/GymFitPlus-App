using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Core.ViewModels.ExerciseViewModels
{
    public class ExerciseAllViewModel : ExerciseForProgramViewModel
    {       
        [Required(ErrorMessage = RequiredErrorMessage)]
        public byte[] Image { get; set; } = null!;

        public int UsedByProgramsCount { get; set; }
    }
}
