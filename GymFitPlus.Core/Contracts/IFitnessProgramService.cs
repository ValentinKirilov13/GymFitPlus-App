using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitPlus.Core.Contracts
{
    public interface IFitnessProgramService
    {
        Task<bool> AddExerciseToProgramAsync(FitnessProgramExercisesInfoViewModel viewModel);
        Task AddFitnessProgramAsync(FitnessProgramFormViewModel viewModel, Guid userId);
        Task<IEnumerable<FitnessProgramFormViewModel>> AllFitnessProgramsAsync(Guid userId);
        Task DeleteFitnessProgramAsync(int id);
        Task<FitnessProgramDetailViewModel> FindFitnessProgramByIdAsync(int id);
        Task<IEnumerable<ExerciseForProgramViewModel>> GetAllExerciseForProgramAsync();
    }
}
