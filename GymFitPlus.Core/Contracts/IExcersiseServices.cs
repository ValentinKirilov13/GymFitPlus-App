using GymFitPlus.Core.ViewModels.ExcersiseViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IExcersiseServices
    {
        Task<ExcersiseViewModel?> FindExcersiseByIdAsync(int id);
        Task<IEnumerable<ExcersiseViewModel>> AllExcersiseAsync();
        Task AddExcersiseAsync(ExcersiseViewModel viewModel);
        Task DeleteExcersiseAsync(int id);
        Task EditExcersiseAsync(ExcersiseViewModel viewModel);
    }
}
