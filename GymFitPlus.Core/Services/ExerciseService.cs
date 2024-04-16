using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IRepository _repository;
        private readonly IFitnessProgramService _fitnessProgramService;

        public ExerciseService(IRepository repository, IFitnessProgramService fitnessProgramService)
        {
            _repository = repository;
            _fitnessProgramService = fitnessProgramService;
        }

        public async Task<bool> AddExerciseAsync(ExerciseDetailViewModel viewModel)
        {
            Exercise model = new Exercise()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                VideoUrl = viewModel.VideoUrl.Split("v=").Reverse().ToArray()[0],
                MuscleGroup = viewModel.MuscleGroup
            };

            await _repository.AddAsync(model);
            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }
        public async Task<bool> EditExerciseAsync(ExerciseDetailViewModel viewModel)
        {
            var model = await FindByIdAsync(viewModel.Id, false);

            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.VideoUrl = viewModel.VideoUrl.Split("v=").Reverse().ToArray()[0];
            model.MuscleGroup = viewModel.MuscleGroup;

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }
        public async Task<bool> DeleteExerciseAsync(int id)
        {
            var model = await FindByIdAsync(id, false);

            model.IsDelete = true;

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }
        public async Task<IEnumerable<ExerciseAllViewModel>> AllExerciseAsync(AllExercisesQueryModel query)
        {
            var model = _repository
                .AllReadOnly<Exercise>()
                .Where(x => x.IsDelete == false)
                .Select(x => new ExerciseAllViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    MuscleGroup = x.MuscleGroup,
                    UsedByProgramsCount = x.FitnessProgramsExercises.Count()
                });

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                string normalizedSearchTerm = query
                                                  .SearchTerm
                                                  .ToLower();
                model = model
                            .Where(m => m.Name
                                            .ToLower()
                                            .Contains(normalizedSearchTerm));
            }

            if (query.Category != default)
            {
                model = model
                            .Where(x => x.MuscleGroup == query.Category);
            }

            model = query.Sorting switch
            {
                Sorting.Interactions => model
                                            .OrderByDescending(m => m.UsedByProgramsCount),
                Sorting.Аlphabetical => model
                                            .OrderBy(m => m.Name),
                _ => model
                         .OrderByDescending(m => m.Id)
            };

            query.TotalExerciseCount = model.Count();

            return await model
                            .Skip((query.CurrentPage - 1) * query.ExercisePerPage)
                            .Take(query.ExercisePerPage)
                            .ToListAsync();
        }
        public async Task<ExerciseDetailViewModel> FindExerciseByIdAsync(int id)
        {
            return await _repository
                .AllReadOnly<Exercise>()
                .Where(x => x.IsDelete == false)
                .Select(x => new ExerciseDetailViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    VideoUrl = x.VideoUrl,
                    MuscleGroup = x.MuscleGroup,
                    UsedByProgramsCount = x.FitnessProgramsExercises.Count()
                })
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();
        }

        public async Task<IEnumerable<ExerciseForProgramViewModel>> GetAllExerciseForProgramAsync(int programId)
        {
            IEnumerable<int> exercisesIdsNotToGet = await _fitnessProgramService.GetAllExerciseFromProgramAsync(programId);

            return await _repository
                .AllReadOnly<Exercise>()
                .Where(x => x.IsDelete == false && !exercisesIdsNotToGet.Contains(x.Id))
                .Select(x => new ExerciseForProgramViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
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

            return affectedRows > 0;
        }
        public async Task<bool> EditExerciseFromProgramAsync(FitnessProgramExercisesInfoViewModel viewModel)
        {
            FitnessProgramExercise model = await FindExerciseFromProgramAsync(viewModel.ExerciseId, viewModel.FitnessProgramId);

            model.Sets = viewModel.Sets;
            model.Reps = viewModel.Reps;
            model.Weight = viewModel.Weight;
            model.Order = viewModel.Order;

            int affectedRows = await _repository.SaveChangesAsync();

            await _fitnessProgramService.ReOrderExercisesInFitnessProgramAsync(viewModel.FitnessProgramId);

            return affectedRows > 0;
        }
        public async Task<bool> RemoveExerciseFromProgramAsync(int exerciseId, int programId)
        {
            FitnessProgramExercise model = await FindExerciseFromProgramAsync(exerciseId, programId);

            _repository.Remove(model);

            int affectedRows = await _repository.SaveChangesAsync();

            await _fitnessProgramService.ReOrderExercisesInFitnessProgramAsync(programId);

            return affectedRows > 0;
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


        public async Task<IEnumerable<ExerciseAllViewModel>> AllExerciseForAdminAsync(bool deleted)
        {
            return await _repository
               .AllReadOnly<Exercise>()
               .Where(x => x.IsDelete == deleted)
               .Select(x => new ExerciseAllViewModel()
               {
                   Id = x.Id,
                   Name = x.Name,
                   MuscleGroup = x.MuscleGroup,
                   UsedByProgramsCount = x.FitnessProgramsExercises.Count()
               })
               .ToListAsync();
        }
        public async Task<bool> RestoreExerciseAsync(int id)
        {
            var model = await FindByIdAsync(id, true);

            model.IsDelete = false;

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }

        private async Task<Exercise> FindByIdAsync(int id, bool deleted)
        {
            return await _repository
                .All<Exercise>()
                .Where(x => x.IsDelete == deleted)
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();
        }
        private async Task<FitnessProgramExercise> FindExerciseFromProgramAsync(int exerciseId, int programId)
        {
            return await _repository
                .All<FitnessProgramExercise>()
                .FirstOrDefaultAsync(x =>
                                x.FitnessProgramId == programId &&
                                x.ExerciseId == exerciseId) ?? throw new NullReferenceException();
        }
    }
}
