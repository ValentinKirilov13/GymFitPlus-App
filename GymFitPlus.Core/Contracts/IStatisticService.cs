using GymFitPlus.Core.ViewModels.StatisticViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitPlus.Core.Contracts
{
    public interface IStatisticService
    {
        Task<IEnumerable<UserStatsViewModel>> GetUserConcreteStatsAsync(string stats, Guid userId);
        Task<UserStatsViewModel> GetUserLastAllStatsAsync(Guid userId);
        Task UpdateUserStatsAsync(UserStatsViewModel viewModel);
    }
}
