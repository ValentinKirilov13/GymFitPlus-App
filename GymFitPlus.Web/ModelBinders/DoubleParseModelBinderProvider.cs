using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymFitPlus.Web.ModelBinders
{
    public class DoubleParseModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(double)
                || context.Metadata.ModelType == typeof(double?))
            {
                return new BinderTypeModelBinder(typeof(DoubleParseModelBinder));
            }

            return null;
        }
    }
}
