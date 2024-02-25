using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.UserInfoViewModels;
using GymFitPlus.Infrastructure.Data;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Core.Services
{
    public class UserInfoServices : IUserInfoServices
    {
        private readonly ApplicationDbContext _context;

        public UserInfoServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(UserInfoViewModel viewModel, string userId)
        {
            var model = new UserInfo()
            {
                Id = userId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                BirthDate = DateTime.Parse(viewModel.BirthDate),
                Gender = (GenderType)viewModel.Gender,
                ImgUrl = viewModel.ImgUrl,
                FacebookUrl = viewModel.FacebookUrl,
                InstagramUrl = viewModel.InstagramUrl,
                YouTubeUrl = viewModel.YouTubeUrl,
            };

            await _context.UserInfos.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<UserInfoViewModel?> GetUserInfoByIdAsync(string userId)
        {
            return await _context.UserInfos
                .AsNoTracking()
                .Where(x => x.Id == userId)
                .Select(x => new UserInfoViewModel()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = (int)x.Gender,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    ImgUrl = x.ImgUrl,
                    InstagramUrl = x.InstagramUrl,
                    FacebookUrl = x.FacebookUrl,
                    YouTubeUrl = x.YouTubeUrl,
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditUserAsync(UserInfoViewModel viewModel, string userId)
        {
            var model = await _context.UserInfos
               .FirstOrDefaultAsync(x => x.Id == userId);
            
            model.Gender = (GenderType)viewModel.Gender;
            model.FacebookUrl = viewModel.FacebookUrl;
            model.InstagramUrl = viewModel.InstagramUrl;
            model.YouTubeUrl = viewModel.YouTubeUrl;
            model.ImgUrl = viewModel.ImgUrl;

            await _context.SaveChangesAsync();
        }
    }
}
