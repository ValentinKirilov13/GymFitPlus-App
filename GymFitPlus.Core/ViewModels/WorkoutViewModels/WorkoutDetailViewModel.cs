using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Infrastructure.Constants.DataConstants.WorkoutConstants;

namespace GymFitPlus.Core.ViewModels.WorkoutViewModels
{
    public class WorkoutDetailViewModel : WorkoutAllViewModel
    {
        [Required]
        public int FitnessProgramId { get; set; }


        [MaxLength(NoteMaxLenght)]
        public string? Note { get; set; }
    }
}
