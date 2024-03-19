namespace GymFitPlus.Core.ViewModels.WorkoutViewModels
{
    public class WorkoutAllViewModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
  
        public string FitnessProgramName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public int Duration { get; set; }
    }
}
