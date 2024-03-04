using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ExcerciseConstants;

namespace GymFitPlus.Core.ViewModels.ExcersiseViewModels
{
    public class ExerciseDetailViewModel : ExerciseAllViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(DescriptionMaxLenght, MinimumLength = DescriptionMinLenght, ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;
    }
}
