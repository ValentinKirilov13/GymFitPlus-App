using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.ExerciseViewModels;
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

        public async Task<FitnessProgramDetailViewModel> FindFitnessProgramByIdAsync(int id)
        {
            return await _repository
                .AllReadOnly<FitnessProgram>()
                .Select(p => new FitnessProgramDetailViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Exercises = p.FitnessProgramsExercises
                    .Where(x => x.FitnessProgramId == id)
                    .Select(x => new FitnessProgramExercisesInfoViewModel()
                    {
                        FitnessProgramId = x.FitnessProgramId,
                        ExerciseId = x.Exercise.Id,
                        ExerciseName = x.Exercise.Name,
                        Reps = x.Reps,
                        Sets = x.Sets,
                        Weight = x.Weight,
                        Order = x.Order
                    })
                    .ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();
        }

        public async Task<IEnumerable<FitnessProgramFormViewModel>> AllFitnessProgramsAsync(Guid userId)
        {
            return await _repository
                .AllReadOnly<FitnessProgram>()
                .Where(x => x.IsDelete == false && x.UserId == userId)
                .Select(x => new FitnessProgramFormViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ExerciseCount = x.FitnessProgramsExercises.Count(),
                })
                .ToListAsync();
        }

        public async Task<bool> AddExerciseToProgramAsync(FitnessProgramExercisesInfoViewModel viewModel)
        {                
            var model = new FitnessProgramExercise()
            {
                FitnessProgramId = viewModel.FitnessProgramId,
                ExerciseId = viewModel.ExerciseId,
                Reps = viewModel.Reps,
                Sets = viewModel.Sets,
                Weight = viewModel.Weight,
                Order = viewModel.Order
            };

            await _repository.AddAsync(model);

            int affectedRows = await _repository.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task EditFitnessProgram(FitnessProgramFormViewModel viewModel)
        {
            var model = await FindByIdAsync(viewModel.Id) ?? throw new NullReferenceException();

            model.Name = viewModel.Name;

            await _repository.SaveChangesAsync();
        }

        public async Task EditFitnessProgramExercise(FitnessProgramExercisesInfoViewModel viewModel)
        {
            var model = _repository
                .All<FitnessProgram>()
                .Where(x => x.Id == viewModel.FitnessProgramId)
                .Select(x => x.FitnessProgramsExercises)
                .First()
                .FirstOrDefault(x =>
                                x.FitnessProgramId == viewModel.FitnessProgramId &&
                                x.ExerciseId == viewModel.ExerciseId) ?? throw new NullReferenceException();

            model.Sets = viewModel.Sets;
            model.Reps = viewModel.Reps;
            model.Weight = viewModel.Weight;
            model.Order = viewModel.Order;

            await _repository.SaveChangesAsync();
        }



        public async Task<FitnessProgramExercisesInfoViewModel> GetExerciseFromProgramToEditAsync(int exerciseId, int programId)
        {
            return await _repository
                .AllReadOnly<FitnessProgramExercise>()
                .Select(x => new FitnessProgramExercisesInfoViewModel()
                {
                    FitnessProgramId = x.FitnessProgramId,
                    ExerciseId = x.ExerciseId,
                    ExerciseName = x.Exercise.Name,
                    Reps = x.Reps,
                    Sets = x.Sets,
                    Weight = x.Weight,
                    Order = x.Order
                })
                .FirstOrDefaultAsync(x =>
                                     x.ExerciseId == exerciseId &&
                                     x.FitnessProgramId == programId) ?? throw new NullReferenceException();
        }

        private async Task<FitnessProgram?> FindByIdAsync(int id)
        {
            return await _repository
                .All<FitnessProgram>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
