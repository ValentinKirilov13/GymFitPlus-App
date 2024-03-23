using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.AccountViewModels;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
        {
            ApplicationUser user = new()
            {
                UserName = model.Username,
                Email = model.Email,
            };

            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<IdentityResult> RegisterUserInfoAsync(RegisterUserInfoFormViewModel model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new NullReferenceException();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;
            user.FacebookUrl = model.FacebookUrl;
            user.InstagramUrl = model.InstagramUrl;
            user.YouTubeUrl = model.YouTubeUrl;
            user.PhoneNumber = model.PhoneNumber;

            if (model.Image != null && model.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.Image[0].CopyTo(memoryStream);
                    user.Image = memoryStream.ToArray();
                }
            }

            return await _userManager.UpdateAsync(user);
        }

        public async Task<bool> IsCurrentUserFullRegisteredAsync(Guid userId)
        {
            var currUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NullReferenceException();

            if (string.IsNullOrEmpty(currUser.FirstName) && string.IsNullOrEmpty(currUser.LastName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<UserInfoViewModel> GetUserForDashboardAsync(Guid userId)
        {
            return await _userManager.Users
               .AsNoTracking()
               .Where(x => x.Id == userId)
               .Select(x => new UserInfoViewModel()
               {
                   FullName = x.FirstName != null && x.LastName != null ? $"{x.FirstName} {x.LastName}" : null,
                   Age = x.BirthDate != null ? DateTime.Today.Year - x.BirthDate.Value.Year : null,
                   Gander = x.Gender != null ? x.Gender.ToString() : null,
                   Image = x.Image,
                   FacebookUrl = x.FacebookUrl,
                   InstagramUrl = x.InstagramUrl,
                   YouTubeUrl = x.YouTubeUrl
               })
               .FirstOrDefaultAsync() ?? throw new NullReferenceException();
        }
    }
}
