using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using System;

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
            Exercise model = new Exercise()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                VideoUrl = viewModel.VideoUrl.Split("v=").Reverse().ToArray()[0],
                MuscleGroup = viewModel.MuscleGroup
            };

            await _repository.AddAsync(model);
            await _repository.SaveChangesAsync();
        }
        public async Task EditExerciseAsync(ExerciseDetailViewModel viewModel)
        {
            var model = await FindByIdAsync(viewModel.Id);

            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.VideoUrl = viewModel.VideoUrl.Split("v=").Reverse().ToArray()[0];
            model.MuscleGroup = viewModel.MuscleGroup;

            await _repository.SaveChangesAsync();
        }
        public async Task DeleteExerciseAsync(int id)
        {
            var model = await FindByIdAsync(id);

            model.IsDelete = true;

            await _repository.SaveChangesAsync();
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
                ExerciseSorting.MostUsed => model
                                                .OrderByDescending(m => m.UsedByProgramsCount),
                ExerciseSorting.A_Z => model
                                            .OrderBy(m => m.Name),
                _ => model
                         .OrderBy(m => m.Id)
            };

            return await model
                            .Skip((query.CurrentPage - 1) * query.ExercisePerPage)
                            .Take(query.ExercisePerPage)
                            .ToListAsync();
        }
        public async Task<int> CountAllExerciseAsync()
        {
            return await _repository
                .AllReadOnly<Exercise>()
                .Where(x => x.IsDelete == false)
                .CountAsync();
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

        private async Task<Exercise> FindByIdAsync(int id)
        {
            return await _repository
                .All<Exercise>()
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();
        }
    }
}
