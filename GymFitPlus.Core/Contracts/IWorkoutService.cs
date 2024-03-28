using GymFitPlus.Core.ViewModels.WorkoutViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IWorkoutService
    {
        Task CreateWorkoutAsync(WorkoutDetailViewModel viewModel);
        Task DeleteWorkoutAsync(WorkoutDetailViewModel viewModel, Guid userId);
        Task<IEnumerable<WorkoutAllViewModel>> GetAllWorkoutsAsync(Guid userId);
        Task<WorkoutDetailViewModel> GetByIdWorkoutAsync(int workoutId, Guid userId);
    }
}
