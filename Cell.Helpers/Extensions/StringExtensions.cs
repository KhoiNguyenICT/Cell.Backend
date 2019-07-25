using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Cell.Helpers.Extensions
{
    public static class Extensions
    {
        public static string JoinString(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }

        public static string ToColumnName(this string prop)
        {
            prop = Regex.Replace(prop, "([a-z])([A-Z])", "$1_$2");
            prop = prop.ToUpper();
            return prop;
        }
    }
}