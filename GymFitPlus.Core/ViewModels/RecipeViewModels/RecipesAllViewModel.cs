using GymFitPlus.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.RecipeConstants;

namespace GymFitPlus.Core.ViewModels.RecipeViewModels
{
    public class RecipesAllViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NameMaxLenght,
            MinimumLength = NameMinLenght,
            ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

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
        public RecipeType Category { get; set; }

        public int FavoriteByUsers { get; set; }

        public byte[] Image { get; set; } = null!;       
    }
}
