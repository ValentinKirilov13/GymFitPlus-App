using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ExcerciseConstants;

namespace GymFitPlus.Core.ViewModels.ExcersiseViewModels
{
    public class ExcersiseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght,ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(DescriptionMaxLenght, MinimumLength = DescriptionMinLenght, ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(ImgUrlMaxLenght, MinimumLength = ImgUrlMinLenght, ErrorMessage = LengthErrorMessage)]
        public string ImgUrl { get; set; } = string.Empty;
    }
}
