using GymFitPlus.Core.ViewModels.WorkoutViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IWorkoutService
    {
        Task<bool> CreateWorkoutAsync(WorkoutDetailViewModel viewModel);
        Task<bool> DeleteWorkoutAsync(WorkoutDetailViewModel viewModel, Guid userId);
        Task<IEnumerable<WorkoutAllViewModel>> GetAllWorkoutsAsync(Guid userId);
        Task<WorkoutDetailViewModel> GetByIdWorkoutAsync(int workoutId, Guid userId);
    }
}
