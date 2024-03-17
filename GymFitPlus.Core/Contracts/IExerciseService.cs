using GymFitPlus.Core.ViewModels.ExerciseViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseAllViewModel>> AllExerciseAsync(AllExercisesQueryModel query);
        Task<ExerciseDetailViewModel> FindExerciseByIdAsync(int id);
        Task AddExerciseAsync(ExerciseDetailViewModel viewModel);
        Task DeleteExerciseAsync(int id);
        Task EditExerciseAsync(ExerciseDetailViewModel viewModel);
        Task<IEnumerable<ExerciseForProgramViewModel>> GetAllExerciseForProgramAsync(IEnumerable<int> exercisesIdsNotToGet);
        Task<int> CountAllExerciseAsync();
    }
}
