using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseAllViewModel>> AllExerciseAsync(AllExercisesQueryModel query);
        Task<ExerciseDetailViewModel> FindExerciseByIdAsync(int id);
        Task<bool> AddExerciseAsync(ExerciseDetailViewModel viewModel);
        Task<bool> DeleteExerciseAsync(int id);
        Task<bool> EditExerciseAsync(ExerciseDetailViewModel viewModel);
        Task<IEnumerable<ExerciseForProgramViewModel>> GetAllExerciseForProgramAsync(int programId);
        Task<bool> AddExerciseToProgramAsync(FitnessProgramExercisesInfoViewModel viewModel);
        Task<bool> EditExerciseFromProgramAsync(FitnessProgramExercisesInfoViewModel viewModel);
        Task<bool> RemoveExerciseFromProgramAsync(int exerciseId, int programId);
        Task<FitnessProgramExercisesInfoViewModel> GetExerciseFromProgramToEditAsync(int exerciseId, int programId);
    }
}
