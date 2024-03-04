using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.FitnessProgramConstants;

namespace GymFitPlus.Core.ViewModels.FitnessProgramViewModels
{
    public class FitnessProgramFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(
            NameMaxLenght,
            MinimumLength = NameMinLenght,
            ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;
    }
}
