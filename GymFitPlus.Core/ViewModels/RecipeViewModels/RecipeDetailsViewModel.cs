using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.RecipeConstants;

namespace GymFitPlus.Core.ViewModels.RecipeViewModels
{
    public class RecipeDetailsViewModel : RecipesAllViewModel
    {        
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(DescriptionMaxLenght,
                      MinimumLength = DescriptionMinLenght,
                      ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(ImageMaxLenght, ErrorMessage = PhotoErrorMessage)]
        public IFormFile[]? ImageForForm { get; set; }
    }
}
