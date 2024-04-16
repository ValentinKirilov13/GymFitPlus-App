using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.StatisticConstants;

namespace GymFitPlus.Core.ViewModels.StatisticViewModels
{
    public class UserStatsViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Display(Name = "Date Of Мeasurements")]
        public DateTime DateOfМeasurements { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        public double Weight { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        public double Height { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Chest Circumference")]
        public double ChestCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Back Circumference")]
        public double BackCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Right Arm Circumference")]
        public double RightArmCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Left Arm Circumference")]
        public double LeftArmCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Waist Circumference")]
        public double WaistCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Gluteus Circumference")]
        public double GluteusCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Right Leg Circumference")]
        public double RightLegCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Left Leg Circumference")]
        public double LeftLegCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Right Calf Circumference")]
        public double RightCalfCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = RangeErrorMessages)]
        [Display(Name = "Left Calf Circumference")]
        public double LeftCalfCircumference { get; set; }
    }
}
