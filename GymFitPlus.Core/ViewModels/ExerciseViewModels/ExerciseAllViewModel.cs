using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ExcerciseConstants;

namespace GymFitPlus.Core.ViewModels.ExcersiseViewModels
{
    public class ExerciseAllViewModel : ExerciseForProgramViewModel
    {       
        [Required(ErrorMessage = RequiredErrorMessage)]
        public byte[] Image { get; set; } = null!;

        public int UsedByProgramsCount { get; set; }
    }
}
