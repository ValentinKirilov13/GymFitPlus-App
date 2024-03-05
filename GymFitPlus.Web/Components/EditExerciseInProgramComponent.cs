using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Components
{
    public class EditExerciseInProgramComponent : ViewComponent
    {
        private readonly IFitnessProgramService _fitnessProgramService;

        public EditExerciseInProgramComponent(IFitnessProgramService fitnessProgramService)
        {
            _fitnessProgramService = fitnessProgramService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int exerciseId, int programId)
        {
            FitnessProgramExercisesInfoViewModel model = await _fitnessProgramService.GetExerciseFromProgramToEditAsync(exerciseId, programId);

            return await Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
