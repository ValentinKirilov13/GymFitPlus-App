﻿using GymFitPlus.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ApplicationUserConstants;

namespace GymFitPlus.Core.ViewModels.AccountViewModels
{
    public class RegisterUserInfoFormViewModel
    {
        [StringLength(
            NameMaxLenght,
            MinimumLength = NameMinLenght,
            ErrorMessage = LengthErrorMessage)]
        [Display(Name = "First name")]
        public string? FirstName { get; set; }

        [StringLength(
            NameMaxLenght,
            MinimumLength = NameMinLenght,
            ErrorMessage = LengthErrorMessage)]
        [Display(Name = "Last name")]
        public string? LastName { get; set; }

        [Display(Name = "Birthdate")]
        public DateTime? BirthDate { get; set; }

        public GenderType? Gender { get; set; }

        [MaxLength(ImageMaxLenght, ErrorMessage = PhotoErrorMessage)]
        public IFormFile[]? Image { get; set; }

        [MaxLength(ImageMaxLenght, ErrorMessage = PhotoErrorMessage)]
        public byte[]? ImageForPreview { get; set; }

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
