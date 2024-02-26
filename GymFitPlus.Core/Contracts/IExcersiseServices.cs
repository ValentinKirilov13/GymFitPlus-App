using GymFitPlus.Core.ViewModels.ExcersiseViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IExcersiseServices
    {
        Task<ExcersiseViewModel?> FindExcersiseByIdAsync(int id);
        Task<IEnumerable<ExcersiseViewModel>> AllExcersiseAsync();
        Task<bool> AddExcersiseAsync(ExcersiseViewModel viewModel);
        Task<bool> DeleteExcersiseAsync(int id);
        Task<bool> EditExcersiseAsync(ExcersiseViewModel viewModel);
    }
}
