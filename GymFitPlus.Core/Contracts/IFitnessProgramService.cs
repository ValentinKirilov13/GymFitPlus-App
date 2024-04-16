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
        Task<IEnumerable<int>> GetAllExerciseFromProgramAsync(int programId);
        Task<IEnumerable<FitnessProgramFormViewModel>> GetAllFitnessProgramsFilltered(Guid userId, int exerciseId);
        Task ReOrderExercisesInFitnessProgramAsync(int programId);
        Task<IEnumerable<FitnessProgramFormViewModel>> GetUserAllDeletedFitnessProgramsAsync(string username);
        Task<bool> RestoreFitnessProgramAsync(int fitnessProgramId);
    }
}
