using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Infrastructure.Constants.DataConstants.StatisticConstants;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Core.ViewModels.StatisticViewModels
{
    public class UserStatsViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public DateTime DateOfМeasurements { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double Weight { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double Height { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double ChestCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double BackCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double RightArmCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double LeftArmCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double WaistCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double GluteusCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double RightLegCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double LeftLegCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double RightCalfCircumference { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(StatsMinValue,
               StatsMaxValue,
               ErrorMessage = StatisticRangeErrorMessages)]
        public double LeftCalfCircumference { get; set; }
    }
}
