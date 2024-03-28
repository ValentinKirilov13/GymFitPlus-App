using GymFitPlus.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.RecipeConstants;

namespace GymFitPlus.Core.ViewModels.RecipeViewModels
{
    public class RecipeDetailsViewModel : RecipesAllViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(MacrosMinValue,
               MacrosMaxValue,
               ErrorMessage = RangeErrorMessages)]
        public double ProteinPerHundredGrams { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(MacrosMinValue,
               MacrosMaxValue,
               ErrorMessage = RangeErrorMessages)]
        public double CarbsPerHundredGrams { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(MacrosMinValue,
               MacrosMaxValue,
               ErrorMessage = RangeErrorMessages)]
        public double FatPerHundredGrams { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(DescriptionMaxLenght,
                      MinimumLength = DescriptionMinLenght,
                      ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public RecipeType Category { get; set; }

        [MaxLength(ImageMaxLenght, ErrorMessage = PhotoErrorMessage)]
        public IFormFile[]? ImageForForm { get; set; }
    }
}
