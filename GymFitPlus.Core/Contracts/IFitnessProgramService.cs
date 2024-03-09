using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IFitnessProgramService
    {
        Task<bool> AddExerciseToProgramAsync(FitnessProgramExercisesInfoViewModel viewModel);
        Task AddFitnessProgramAsync(FitnessProgramFormViewModel viewModel, Guid userId);
        Task<IEnumerable<FitnessProgramFormViewModel>> AllFitnessProgramsAsync(Guid userId);
        Task DeleteFitnessProgramAsync(int id);
        Task EditFitnessProgram(FitnessProgramFormViewModel viewModel);
        Task EditFitnessProgramExercise(FitnessProgramExercisesInfoViewModel viewModel);
        Task<FitnessProgramDetailViewModel> FindFitnessProgramByIdAsync(int id);
        Task<FitnessProgramExercisesInfoViewModel> GetExerciseFromProgramToEditAsync(int exerciseId, int programId);
        Task RemoveExerciseFromProgramAsync(int exerciseId, int programId);
    }
}
