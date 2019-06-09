using Newtonsoft.Json;

namespace Cell.Core.Extensions
{
    public static class JsonExtension
    {
        public static T TryDeserialize<T>(this string str)
        {
            return string.IsNullOrEmpty(str) ? default(T) : JsonConvert.DeserializeObject<T>(str);
        }

        public static T TryDeserialize<T>(this string str, JsonSerializerSettings settings)
        {
            return string.IsNullOrEmpty(str) ? default(T) : JsonConvert.DeserializeObject<T>(str, settings);
        }
    }
}
