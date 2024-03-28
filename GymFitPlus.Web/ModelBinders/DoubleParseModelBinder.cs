using Microsoft.AspNetCore.Mvc.ModelBinding;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Web.ModelBinders
{
    public class DoubleParseModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
               .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                double result = default;
                bool success = false;

                try
                {
                    string strValue = valueResult.FirstValue.ToString().Replace(',','.');

                    success = double.TryParse(
                              strValue,
                              out result);
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }

                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
                else
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid value!");
                }
            }
            else if(string.IsNullOrEmpty(valueResult.FirstValue))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, string.Format(RequiredErrorMessage, bindingContext.ModelMetadata.DisplayName));
            }


            return Task.CompletedTask;
        }
    }
}
