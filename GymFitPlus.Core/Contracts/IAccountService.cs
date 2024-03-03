using GymFitPlus.Core.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;

namespace GymFitPlus.Core.Contracts
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
        Task<IdentityResult> RegisterUserInfoAsync(RegisterUserInfoFormViewModel model, string userId);
    }
}
