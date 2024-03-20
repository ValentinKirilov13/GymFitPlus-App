using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Infrastructure.Constants.DataConstants.WorkoutConstants;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Core.ViewModels.WorkoutViewModels
{
    public class WorkoutDetailViewModel : WorkoutAllViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public int FitnessProgramId { get; set; }


        [StringLength(NoteMaxLenght,
            MinimumLength = NoteMinLenght,
            ErrorMessage = LengthErrorMessage)]
        public string? Note { get; set; }
    }
}
