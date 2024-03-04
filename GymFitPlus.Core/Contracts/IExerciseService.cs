using GymFitPlus.Core.ViewModels.ExcersiseViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IExerciseService
    {
        Task<ExerciseDetailViewModel?> FindExcersiseByIdAsync(int id);
        Task<IEnumerable<ExerciseAllViewModel>> AllExcersiseAsync();
        Task AddExcersiseAsync(ExerciseDetailViewModel viewModel);
        Task DeleteExcersiseAsync(int id);
        Task EditExcersiseAsync(ExerciseDetailViewModel viewModel);
    }
}
