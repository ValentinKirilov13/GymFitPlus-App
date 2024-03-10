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

        public async Task AddExerciseAsync(ExerciseDetailViewModel viewModel)
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
        public async Task DeleteExerciseAsync(int id)
        {
            var model = await FindByIdAsync(id) ?? throw new NullReferenceException();

            model.IsDelete = true;

            await _repository.SaveChangesAsync();
        }
        public async Task EditExerciseAsync(ExerciseDetailViewModel viewModel)
        {
            var model = await FindByIdAsync(viewModel.Id) ?? throw new NullReferenceException();

            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.Image = viewModel.Image;

            await _repository.SaveChangesAsync();
        }


        public async Task<ExerciseDetailViewModel?> FindExerciseByIdAsync(int id)
        {
            return await _repository.AllReadOnly<Exercise>()               
                .Where(x => x.IsDelete == false)
                .Select(x => new ExerciseDetailViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    UsedByProgramsCount = x.FitnessProgramsExercises.Count()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<ExerciseAllViewModel>> AllExerciseAsync()
        {
            return await _repository.AllReadOnly<Exercise>()
                .Where(x => x.IsDelete == false)
                .Select(x => new ExerciseAllViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    UsedByProgramsCount = x.FitnessProgramsExercises.Count()
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<ExerciseForProgramViewModel>> GetAllExerciseForProgramAsync(IEnumerable<int> exercisesIdsNotToGet)
        {
            return await _repository
                .AllReadOnly<Exercise>()
                .Where(x => !exercisesIdsNotToGet.Contains(x.Id))
                .Select(x => new ExerciseForProgramViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
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
