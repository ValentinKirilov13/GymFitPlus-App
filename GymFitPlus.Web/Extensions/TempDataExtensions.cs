using System.Text.Json;

namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }

        public static T? Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {          
            tempData.TryGetValue(key, out object? obj);
            return obj == null ? null : JsonSerializer.Deserialize<T>((string)obj);
        }
    }
}
