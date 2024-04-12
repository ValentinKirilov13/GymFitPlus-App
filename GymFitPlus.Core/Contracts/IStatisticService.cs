using GymFitPlus.Core.ViewModels.StatisticViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IStatisticService
    {
        Task<IEnumerable<UserStatsViewModel>> GetUserConcreteStatsAsync(string stats, Guid userId);
        Task<UserStatsViewModel> GetUserLastAllStatsAsync(Guid userId);
        Task<bool> UpdateUserStatsAsync(UserStatsViewModel viewModel);
    }
}
