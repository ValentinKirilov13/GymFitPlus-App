using GymFitPlus.Core.ViewModels.ExerciseViewModels;

namespace GymFitPlus.Core.ViewModels.FitnessProgramViewModels
{
    public class FitnessProgramDetailViewModel : FitnessProgramFormViewModel
    {
        public List<ExerciseForProgramViewModel> Exercises { get; set; }

    }
}
