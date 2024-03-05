using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ExerciseConstants;

namespace GymFitPlus.Core.ViewModels.ExerciseViewModels
{
    public class ExerciseDetailViewModel : ExerciseAllViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(DescriptionMaxLenght, MinimumLength = DescriptionMinLenght, ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;
    }
}
