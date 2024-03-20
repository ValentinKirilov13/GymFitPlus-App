using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Infrastructure.Constants.DataConstants.WorkoutConstants;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Core.ViewModels.WorkoutViewModels
{
    public class WorkoutAllViewModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string FitnessProgramName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        [Display(Name = "Duration of workout")]
        [Range(DurationMinValue, DurationMaxValue, ErrorMessage = DurationErrorMessages)]
        public int Duration { get; set; }
    }
}
