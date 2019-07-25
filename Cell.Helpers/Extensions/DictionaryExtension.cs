using System.Collections.Generic;

namespace Cell.Helpers.Extensions
{
    public static class DictionaryExtension
    {
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd)
        {
            foreach (var (key, value) in dicToAdd)
            {
                dic[key] = value;
            }
        }
    }
}