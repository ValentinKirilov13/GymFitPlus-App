using GymFitPlus.Core.ViewModels.ExerciseViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IExerciseService
    {
        Task<ExerciseDetailViewModel?> FindExerciseByIdAsync(int id);
        Task<IEnumerable<ExerciseAllViewModel>> AllExerciseAsync();
        Task AddExerciseAsync(ExerciseDetailViewModel viewModel);
        Task DeleteExerciseAsync(int id);
        Task EditExerciseAsync(ExerciseDetailViewModel viewModel);
    }
}
