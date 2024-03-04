namespace GymFitPlus.Core.ViewModels.FitnessProgramViewModels
{
    public class FitnessProgramDetailViewModel : FitnessProgramFormViewModel
    {
        public List<FitnessProgramExercisesInfoViewModel> Exercises { get; set; } = new List<FitnessProgramExercisesInfoViewModel>();
    }
}
