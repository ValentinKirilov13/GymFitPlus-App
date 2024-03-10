using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IFitnessProgramService
    {
        Task<bool> AddFitnessProgramAsync(FitnessProgramFormViewModel viewModel, Guid userId);
        Task<bool> EditFitnessProgramAsync(FitnessProgramFormViewModel viewModel);
        Task<bool> DeleteFitnessProgramAsync(int id);
        Task<FitnessProgramDetailViewModel> FindFitnessProgramByIdAsync(int id);
        Task<IEnumerable<FitnessProgramFormViewModel>> AllFitnessProgramsAsync(Guid userId);

        Task<bool> AddExerciseToProgramAsync(FitnessProgramExercisesInfoViewModel viewModel);
        Task<bool> EditExerciseFromProgramAsync(FitnessProgramExercisesInfoViewModel viewModel);
        Task<bool> RemoveExerciseFromProgramAsync(int exerciseId, int programId);
        Task<FitnessProgramExercisesInfoViewModel> GetExerciseFromProgramToEditAsync(int exerciseId, int programId);
        Task<IEnumerable<int>> GetAllExerciseFromProgramAsync(int programId);
    }
}
