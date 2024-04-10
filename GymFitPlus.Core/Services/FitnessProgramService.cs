using GymFitPlus.Core.Contracts;
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

        public async Task<bool> AddFitnessProgramAsync(FitnessProgramFormViewModel viewModel, Guid userId)
        {
            var fitnessProgram = new FitnessProgram()
            {
                Name = viewModel.Name,
                UserId = userId,
            };

            await _repository.AddAsync(fitnessProgram);

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }
        public async Task<bool> EditFitnessProgramAsync(FitnessProgramFormViewModel viewModel)
        {
            var model = await FindByIdAsync(viewModel.Id, deleted: false);

            model.Name = viewModel.Name;

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }
        public async Task<bool> DeleteFitnessProgramAsync(int id)
        {
            var model = await FindByIdAsync(id, deleted: false);

            model.IsDelete = true;

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }
        public async Task<FitnessProgramDetailViewModel> FindFitnessProgramByIdAsync(int id)
        {
            return await _repository
                .AllReadOnly<FitnessProgram>()
                .Where(x => x.IsDelete == false)
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
        public async Task<IEnumerable<int>> GetAllExerciseFromProgramAsync(int programId)
        {
            return await _repository
                .AllReadOnly<FitnessProgram>()
                .Where(x => x.Id == programId)
                .Select(x => x.FitnessProgramsExercises.Select(x => x.ExerciseId))
                .FirstAsync();
        }
        public async Task<IEnumerable<FitnessProgramFormViewModel>> GetAllFitnessProgramsFilltered(Guid userId, int exerciseId)
        {
            return await _repository
               .AllReadOnly<FitnessProgram>()
               .Where(x => x.IsDelete == false &&
                           x.UserId == userId &&
                          !x.FitnessProgramsExercises
                                                    .Select(x => x.ExerciseId)
                                                    .Contains(exerciseId))
               .Select(x => new FitnessProgramFormViewModel()
               {
                   Id = x.Id,
                   Name = x.Name,
                   ExerciseCount = x.FitnessProgramsExercises.Count(),
               })
               .ToListAsync();
        }
        public async Task ReOrderExercisesInFitnessProgramAsync(int programId)
        {
            int startOrder = 1;

            var model = await _repository
                .All<FitnessProgram>()
                .Where(x => x.Id == programId)
                .Select(x => x.FitnessProgramsExercises.OrderBy(x => x.Order).ToList())
                .FirstAsync();

            foreach (var item in model)
            {
                item.Order = startOrder++;
            }

            await _repository.SaveChangesAsync();
        }
        public async Task<IEnumerable<FitnessProgramFormViewModel>> GetUserAllDeletedFitnessProgramsAsync(string username)
        {
            return await _repository
                .AllReadOnly<FitnessProgram>()
                .Where(x => x.IsDelete == true && x.User.UserName == username)
                .Select(x => new FitnessProgramFormViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync(); 
        }
        public async Task<bool> RestoreFitnessProgramAsync(int fitnessProgramId)
        {
            var model = await FindByIdAsync(fitnessProgramId, deleted: true);

            model.IsDelete = false;

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }

        private async Task<FitnessProgram> FindByIdAsync(int id, bool deleted)
        {
            return await _repository
                .All<FitnessProgram>()
                .Where(x => x.IsDelete == deleted)
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();
        }
    }
}
