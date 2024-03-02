using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ApplicationUserConstants;

namespace GymFitPlus.Web.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                DateTime result = DateTime.Today;
                bool success = false;

                try
                {
                    string strValue = valueResult.FirstValue.Trim();

                    success = DateTime.TryParseExact(
                              strValue,
                              DateFormat,
                              CultureInfo.InvariantCulture, 
                              DateTimeStyles.None,
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
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, DateFormatErrorMessage);
                }
            }

            return Task.CompletedTask;
        }
    }
}
