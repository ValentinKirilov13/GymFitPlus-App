namespace GymFitPlus.Core.ViewModels.FitnessProgramViewModels
{
    public class QueryFitnessProgramViewModel
    {
        public string Username { get; set; } = string.Empty;
        public IEnumerable<FitnessProgramFormViewModel> UserDeletedFitnessPrograms { get; set; } = new List<FitnessProgramFormViewModel>();
    }
}
