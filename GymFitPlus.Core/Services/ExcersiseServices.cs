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

        public async Task AddExcersiseAsync(ExcersiseDetailViewModel viewModel)
        {
            var model = new Excercise()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Image = viewModel.Image,
            };

            await _context.Excercises.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteExcersiseAsync(int id)
        {
            var model = await FindByIdAsync(id) ?? throw new Exception("The excersise didn't exist!");

            model.IsDelete = true;

            await _context.SaveChangesAsync();
        }
        public async Task EditExcersiseAsync(ExcersiseDetailViewModel viewModel)
        {
            var model = await FindByIdAsync(viewModel.Id) ?? throw new Exception("The excersise didn't exist!");

            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.Image = viewModel.Image;

            await _context.SaveChangesAsync();
        }


        public async Task<ExcersiseDetailViewModel?> FindExcersiseByIdAsync(int id)
        {
            return await _context
                .Excercises
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .Select(x => new ExcersiseDetailViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    UsedByProgramsCount = x.FitnessProgramsExcercises.Count()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ExcersiseAllViewModel>> AllExcersiseAsync()
        {
            return await _context
                .Excercises
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .Select(x => new ExcersiseAllViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    UsedByProgramsCount = x.FitnessProgramsExcercises.Count()
                })
                .ToListAsync();
        }


        
        private async Task<Excercise?> FindByIdAsync(int id)
        {
            return await _context.Excercises
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
