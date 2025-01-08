using System.Text.Json;

namespace Juliapos.Tracking.Api
{
    /// <summary>
    /// Json handling extensions
    /// </summary>
    public static class JsonUtilityExtensions
    {
        /// <summary>
        /// Convert a string to camel case
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string value)
        {
            return value.ToLowerInvariant().Replace("_","");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool PropertyExists(this JsonElement element, string key)
        {
            return element.TryGetProperty(key.ToCamelCase(), out var _);
        }


        ///// <summary>
        ///// Get a string value from a json element
        ///// </summary>
        ///// <param name="element"></param>
        ///// <param name="key"></param>
        ///// <param name="defaultValue"></param>
        ///// <returns></returns>
        //public static string GetStringValue(this JsonElement element, string key, string defaultValue)
        //{
        //    if (element.TryGetProperty(key.ToCamelCase(), out var nameElement))
        //        return nameElement.GetString();
        //    else
        //        return defaultValue;
        //}

        ///// <summary>
        ///// Get an int value from a json element
        ///// </summary>
        ///// <param name="element"></param>
        ///// <param name="key"></param>
        ///// <param name="defaultValue"></param>
        ///// <returns></returns>
        //public static int GetIntValue(this JsonElement element, string key, int defaultValue)
        //{
        //    if (element.TryGetProperty(key.ToCamelCase(), out var nameElement))
        //        return nameElement.GetInt32();
        //    else
        //        return defaultValue;
        //}

        ///// <summary>
        ///// Get an bool value from a json element
        ///// </summary>
        ///// <param name="element"></param>
        ///// <param name="key"></param>
        ///// <param name="defaultValue"></param>
        ///// <returns></returns>
        //public static bool GetBoolValue(this JsonElement element, string key, bool defaultValue)
        //{
        //    if (element.TryGetProperty(key.ToCamelCase(), out var nameElement))
        //        return nameElement.GetBoolean();
        //    else
        //        return defaultValue;
        //}

    }
}
