using GymFitPlus.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ApplicationUserConstants;

namespace GymFitPlus.Core.ViewModels.AccountViewModels
{
    public class RegisterUserInfoFormViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(
            NameMaxLenght,
            MinimumLength = NameMinLenght,
            ErrorMessage = LengthErrorMessage)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(
            NameMaxLenght,
            MinimumLength = NameMinLenght,
            ErrorMessage = LengthErrorMessage)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(GenderTypeEnumMinValue, GenderTypeEnumMaxValue)]
        public GenderType Gender { get; set; }

        [MaxLength(int.MaxValue, ErrorMessage = PhotoErrorMessage)]
        public IFormFile[]? Image { get; set; }

        [StringLength(
           UrlMaxLenght,
           MinimumLength = UrlMinLenght,
           ErrorMessage = LengthErrorMessage)]
        [Display(Name = "Link to Facebook account")]
        [Url]
        public string? FacebookUrl { get; set; }

        [StringLength(
             UrlMaxLenght,
             MinimumLength = UrlMinLenght,
             ErrorMessage = LengthErrorMessage)]
        [Display(Name = "Link to Instagram account")]
        [Url]
        public string? InstagramUrl { get; set; }

        [StringLength(
             UrlMaxLenght,
             MinimumLength = UrlMinLenght,
             ErrorMessage = LengthErrorMessage)]
        [Display(Name = "Link to YouTube account")]
        [Url]
        public string? YouTubeUrl { get; set; }

        [StringLength(UserPhoneMaxLength,
            MinimumLength = UserPhoneMinLength,
            ErrorMessage = LengthErrorMessage)]
        [Display(Name = "Phone number")]
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
