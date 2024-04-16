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

        public async Task<RegisterUserInfoFormViewModel> GetUserInfoForEditAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new NullReferenceException();

            var viewModel = new RegisterUserInfoFormViewModel();

            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            viewModel.BirthDate = user.BirthDate;
            viewModel.Gender = user.Gender;
            viewModel.FacebookUrl = user.FacebookUrl;
            viewModel.InstagramUrl = user.InstagramUrl;
            viewModel.YouTubeUrl = user.YouTubeUrl;
            viewModel.PhoneNumber = user.PhoneNumber;
            viewModel.ImageForPreview = user.Image;
            
            return viewModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(x => new UserViewModel
                {
                    UserName = x.UserName,
                    Email = x.Email
                })
                .ToListAsync();
        }
    }
}
