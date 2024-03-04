using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IRepository _repository;

        public ExerciseService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task AddExcersiseAsync(ExerciseDetailViewModel viewModel)
        {
            var model = new Exercise()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Image = viewModel.Image,
            };

            await _repository.AddAsync(model);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteExcersiseAsync(int id)
        {
            var model = await FindByIdAsync(id) ?? throw new NullReferenceException();

            model.IsDelete = true;

            await _repository.SaveChangesAsync();
        }

        public async Task EditExcersiseAsync(ExerciseDetailViewModel viewModel)
        {
            var model = await FindByIdAsync(viewModel.Id) ?? throw new NullReferenceException();

            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.Image = viewModel.Image;

            await _repository.SaveChangesAsync();
        }


        public async Task<ExerciseDetailViewModel?> FindExcersiseByIdAsync(int id)
        {
            return await _repository.AllReadOnly<Exercise>()               
                .Where(x => x.IsDelete == false)
                .Select(x => new ExerciseDetailViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    UsedByProgramsCount = x.FitnessProgramsExcercises.Count()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<ExerciseAllViewModel>> AllExcersiseAsync()
        {
            return await _repository.AllReadOnly<Exercise>()
                .Where(x => x.IsDelete == false)
                .Select(x => new ExerciseAllViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    UsedByProgramsCount = x.FitnessProgramsExcercises.Count()
                })
                .ToListAsync();
        }


        
        private async Task<Exercise?> FindByIdAsync(int id)
        {
            return await _repository.All<Exercise>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
