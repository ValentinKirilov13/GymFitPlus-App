using GymFitPlus.Core.ViewModels.ExcersiseViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IExcersiseServices
    {
        Task<ExcersiseDetailViewModel?> FindExcersiseByIdAsync(int id);
        Task<IEnumerable<ExcersiseAllViewModel>> AllExcersiseAsync();
        Task AddExcersiseAsync(ExcersiseDetailViewModel viewModel);
        Task DeleteExcersiseAsync(int id);
        Task EditExcersiseAsync(ExcersiseDetailViewModel viewModel);
    }
}
