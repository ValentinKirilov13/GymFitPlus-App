using GymFitPlus.Core.ViewModels.UserInfoViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IUserInfoServices
    {
        Task CreateUserAsync(UserInfoViewModel viewModel, string userId);
        Task EditUserAsync(UserInfoViewModel viewModel, string userId);
        Task<UserInfoViewModel?> GetUserInfoByIdAsync(string userId);
    }
}
