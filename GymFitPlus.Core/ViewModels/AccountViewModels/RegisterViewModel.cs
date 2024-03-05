using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ApplicationUserConstants;

namespace GymFitPlus.Core.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = EmailErrorMessage)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(PasswordMaxLenght, MinimumLength = PasswordMinLenght, ErrorMessage = LengthErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = ConfirmPassErrorMessage)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
