using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.ExcersiseViewModels;
using GymFitPlus.Infrastructure.Data;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Core.Services
{
    public class ExcersiseServices : IExcersiseServices
    {
        private readonly ApplicationDbContext _context;

        public ExcersiseServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddExcersiseAsync(ExcersiseViewModel viewModel)
        {
            bool IsSuccessed;

            try
            {
                var model = new Excercise()
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    ImgUrl = viewModel.ImgUrl,                
                };

                await _context.Excercises.AddAsync(model);
                await _context.SaveChangesAsync();

                IsSuccessed = true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
            
            return IsSuccessed;
        }

        public async Task<ExcersiseViewModel?> FindExcersiseByIdAsync(int id)
        {
            return await _context
                .Excercises
                .Where(x => x.IsDelete == false)
                .Select(x => new ExcersiseViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImgUrl = x.ImgUrl
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ExcersiseViewModel>> AllExcersiseAsync()
        {
            return await _context
                .Excercises
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .Select(x => new ExcersiseViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImgUrl = x.ImgUrl
                })
                .ToListAsync();
        }

        public async Task<bool> EditExcersiseAsync(ExcersiseViewModel viewModel)
        {
            bool IsSuccessed;

            try
            {
                var model = await FindByIdAsync(viewModel.Id) ?? throw new Exception();

                model.Name = viewModel.Name;
                model.Description = viewModel.Description;
                model.ImgUrl = viewModel.ImgUrl;

                await _context.SaveChangesAsync();

                IsSuccessed = true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
            
            return IsSuccessed;
        }

        public async Task<bool> DeleteExcersiseAsync(int id)
        {
            bool IsSuccessed;

            try
            {
                var model = await FindByIdAsync(id) ?? throw new Exception();

                model.IsDelete = true;

                await _context.SaveChangesAsync();

                IsSuccessed = true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }

            return IsSuccessed;
        }

        private async Task<Excercise?> FindByIdAsync(int id)
        {
            return await _context.Excercises
                .Where(x => x.IsDelete == false)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
