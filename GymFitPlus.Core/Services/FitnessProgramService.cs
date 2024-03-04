using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.ExcersiseViewModels;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Core.Services
{
    public class FitnessProgramService : IFitnessProgramService
    {
        private readonly IRepository _repository;

        public FitnessProgramService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task AddFitnessProgramAsync(FitnessProgramFormViewModel viewModel, Guid userId)
        {
            var fitnessProgram = new FitnessProgram()
            {
                Name = viewModel.Name,
                UserId = userId,
            };

            await _repository.AddAsync(fitnessProgram);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteFitnessProgramAsync(int id)
        {
            var model = await FindByIdAsync(id) ?? throw new NullReferenceException();

            model.IsDelete = true;

            await _repository.SaveChangesAsync();
        }

        public async Task<ExerciseDetailViewModel?> FindFitnessProgramByIdAsync(int id)
        {
            return await _repository.AllReadOnly<FitnessProgram>()
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

        private async Task<FitnessProgram?> FindByIdAsync(int id)
        {
            return await _repository.All<FitnessProgram>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
