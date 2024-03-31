using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Components
{
    public class EditExerciseFromProgramComponent : ViewComponent
    {
        private readonly IExerciseService _exerciseService;

        public EditExerciseFromProgramComponent(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public async Task<IViewComponentResult> InvokeAsync(
             Dictionary<string, string> errors,
             int exerciseId,
             int programId)
        {
            FitnessProgramExercisesInfoViewModel viewModel = await _exerciseService.GetExerciseFromProgramToEditAsync(exerciseId, programId);

            if (errors != null)
            {
                foreach (var er in errors)
                {
                    ModelState.AddModelError(er.Key, er.Value);
                }
            }
            
            return await Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    }
}
