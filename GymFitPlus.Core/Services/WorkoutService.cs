using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.WorkoutViewModels;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Core.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IRepository _repository;

        public WorkoutService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WorkoutAllViewModel>> GetAllWorkoutsAsync(Guid userId)
        {
            return await _repository
                .AllReadOnly<Workout>()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.Id)
                .Select(x => new WorkoutAllViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FitnessProgramName = x.FitnessProgram.Name,
                    Date = x.Date,
                    Duration = x.Duration
                })
                .ToListAsync();
        }
        public async Task<WorkoutDetailViewModel> GetByIdWorkoutAsync(int workoutId, Guid userId)
        {
            return await _repository
                .AllReadOnly<Workout>()
                .Select(x => new WorkoutDetailViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FitnessProgramName = x.FitnessProgram.Name,
                    Date = x.Date,
                    Duration = x.Duration,
                    FitnessProgramId = x.FitnessProgramId,
                    Note = x.Note
                })
                .FirstOrDefaultAsync(x => x.Id == workoutId && x.UserId == userId) ?? throw new NullReferenceException();
        }
        public async Task CreateWorkoutAsync(WorkoutDetailViewModel viewModel)
        {
            Workout model = new()
            {
                UserId = viewModel.UserId,
                FitnessProgramId = viewModel.FitnessProgramId,
                Date = viewModel.Date,
                Duration = viewModel.Duration,
                Note = viewModel.Note,
            };

            await _repository.AddAsync(model);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteWorkoutAsync(WorkoutDetailViewModel viewModel, Guid userId)
        {
            Workout workoutToDelete = await _repository
                .All<Workout>()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id && x.UserId == userId) ?? throw new NullReferenceException();

             _repository.Remove(workoutToDelete);
            await _repository.SaveChangesAsync();
        }
    }
}
