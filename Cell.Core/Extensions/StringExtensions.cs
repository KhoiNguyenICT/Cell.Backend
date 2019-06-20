using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Cell.Core.Utils;

namespace Cell.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Interpolate(this string template, object data)
        {
            if (data == null)
            {
                return template;
            }
            IDictionary<string, object> dictionary;
            if (data is IDictionary<string, object> objects)
            {
                dictionary = objects;
            }
            else
            {
                dictionary = data.GetType()
                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                     .ToDictionary(prop => prop.Name.ToUpper(), prop => prop.GetValue(data, null));
            }
            var regex = new Regex(@"\[([^\]]+)\]", RegexOptions.IgnoreCase);
            return regex.Replace(template, match =>
            {
                var key = match.Groups[1].ToString().ToUpper();
                return !dictionary.ContainsKey(key) ? match.ToString() : dictionary[key]?.ToString();
            });
        }

        public static string Duplicate(this string str)
        {
            return $"{str} (1)";
        }

        public static string ToFriendlyFileName(this string fileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            return $"{fileNameWithoutExtension.ToSlug()}{extension}";
        }

        public static string ToUniqueFileName(this string fileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            return $"{fileNameWithoutExtension}-{Guid.NewGuid()}{extension}";
        }

        /// <summary>
        /// A nicer way of calling <see cref="System.String.IsNullOrEmpty(string)"/>
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// A nicer way of calling the inverse of <see cref="System.String.IsNullOrEmpty(string)"/>
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is not null or an empty string (""); otherwise, false.</returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }

        /// <summary>
        /// return prefix ts_query. Eg "search ab" become "search&ab:*"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToTsQueryFormat(this string value)
        {
            var space = new Regex(@"\s+");
            return $"{space.Replace(value.Trim(), "&")}";
        }

        /// <summary>
        /// A nicer way of calling <see cref="System.String.Format(string, object[])"/>
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// Allows for using strings in null coalescing operations
        /// </summary>
        /// <param name="value">The string value to check</param>
        /// <returns>Null if <paramref name="value"/> is empty or the original value of <paramref name="value"/>.</returns>
        public static string NullIfEmpty(this string value)
        {
            if (value == string.Empty)
                return null;

            return value;
        }

        /// <summary>
        /// Slugifies a string
        /// </summary>
        /// <param name="value">The string value to slugify</param>
        /// <param name="maxLength">An optional maximum length of the generated slug</param>
        /// <returns>A URL safe slug representation of the input <paramref name="value"/>.</returns>
        public static string ToSlug(this string value, int? maxLength = null)
        {
            Ensure.Argument.NotNull(value, "value");

            // if it's already a valid slug, return it
            if (RegexUtils.SlugRegex.IsMatch(value))
                return value;

            return GenerateSlug(value, maxLength);
        }

        /// <summary>
        /// Converts a string into a slug that allows segments e.g. <example>.blog/2012/07/01/title</example>.
        /// Normally used to validate user entered slugs.
        /// </summary>
        /// <param name="value">The string value to slugify</param>
        /// <returns>A URL safe slug with segments.</returns>
        public static string ToSlugWithSegments(this string value)
        {
            Ensure.Argument.NotNull(value, "value");

            var segments = value.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var result = segments.Aggregate(string.Empty, (slug, segment) => slug += "/" + segment.ToSlug());
            return result.Trim('/');
        }

        public static string ToCamelCasing(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Separates a PascalCase string
        /// </summary>
        /// <example>
        /// "ThisIsPascalCase".SeparatePascalCase(); // returns "This Is Pascal Case"
        /// </example>
        /// <param name="value">The value to split</param>
        /// <returns>The original string separated on each uppercase character.</returns>
        public static string SeparatePascalCase(this string value)
        {
            Ensure.Argument.NotNullOrEmpty(value, "value");
            return Regex.Replace(value, "([A-Z])", " $1").Trim();
        }

        public static string ToSnakeCasing(this string source)
        {
            return new Regex(@"([^^])([A-Z])", RegexOptions.CultureInvariant)
                .Replace(source, (m) => $"{m.Groups[1]}_{m.Groups[2]}")
                .ToLowerInvariant();
        }

        /// <summary>
        /// Credit for this method goes to http://stackoverflow.com/questions/2920744/url-slugify-alrogithm-in-cs
        /// </summary>
        private static string GenerateSlug(string value, int? maxLength = null)
        {
            // prepare string, remove accents, lower case and convert hyphens to whitespace
            var result = value.RemoveDiacritics().Replace("-", " ").ToLowerInvariant();

            result = Regex.Replace(result, @"[^a-z0-9\s-]", string.Empty); // remove invalid characters
            result = Regex.Replace(result, @"\s+", " ").Trim(); // convert multiple spaces into one space

            if (maxLength.HasValue) // cut and trim
                result = result.Substring(0, result.Length <= maxLength ? result.Length : maxLength.Value).Trim();

            return Regex.Replace(result, @"\s", "-"); // replace all spaces with hyphens
        }

        /// <summary>
        /// Returns a string array containing the trimmed substrings in this <paramref name="value"/>
        /// that are delimited by the provided <paramref name="separators"/>.
        /// </summary>
        public static IEnumerable<string> SplitAndTrim(this string value, params char[] separators)
        {
            Ensure.Argument.NotNull(value, "source");
            return value.Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
        }

        /// <summary>
        /// Checks if the <paramref name="source"/> contains the <paramref name="input"/> based on the provided <paramref name="comparison"/> rules.
        /// </summary>
        public static bool Contains(this string source, string input, StringComparison comparison)
        {
            return source.IndexOf(input, comparison) >= 0;
        }

        public static string NormalizeForSearch(this string source)
        {
            return source.RemoveDiacritics().ToUpper();
        }

        public static string RemoveAccent(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return string.Concat(value.Normalize(NormalizationForm.FormD).Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)).Normalize(NormalizationForm.FormC);
        }

        public static string RemoveDiacritics(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            try
            {
                str = str.ToLower().Trim();
                str = Regex.Replace(str, @"[\r|\n]", " ");
                str = Regex.Replace(str, @"\s+", " ");
                str = Regex.Replace(str, "[áàảãạâấầẩẫậăắằẳẵặ]", "a");
                str = Regex.Replace(str, "[éèẻẽẹêếềểễệ]", "e");
                str = Regex.Replace(str, "[iíìỉĩị]", "i");
                str = Regex.Replace(str, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                str = Regex.Replace(str, "[úùủũụưứừửữự]", "u");
                str = Regex.Replace(str, "[yýỳỷỹỵ]", "y");
                str = Regex.Replace(str, "[đ]", "d");

                //Remove special character
                str = Regex.Replace(str, "[\"`~!@#$%^&*()-+=?/>.<,{}[]|]\\]", "");
                str = str.Replace("̀", "").Replace("̣", "").Replace("̉", "").Replace("̃", "").Replace("́", "");
                return str;
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        /// Limits the length of the <paramref name="source"/> to the specified <paramref name="maxLength"/>.
        /// </summary>
        public static string Limit(this string source, int maxLength, string suffix = null)
        {
            if (suffix.IsNotNullOrEmpty())
            {
                if (suffix != null) maxLength = maxLength - suffix.Length;
            }

            if (source.Length <= maxLength)
            {
                return source;
            }

            return string.Concat(source.Substring(0, maxLength).Trim(), suffix ?? string.Empty);
        }

        public static T ParseEnum<T>(this string value, T defaultValue = default(T)) where T : struct, IConvertible
        {
            return Enum.TryParse<T>(value, true, out var result) ? result : defaultValue;
        }

        private static readonly Regex TagsExpression = new Regex(@"</?.+?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripHtml(this string source)
        {
            return TagsExpression.Replace(source, string.Empty);
        }

        public static Guid? ToGuid(this string str)
        {
            if (Guid.TryParse(str, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static string ToSearchQuery(this string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return null;
            query = Regex.Replace(query, "\\s+", " ");
            query = $"%{query.Trim().Replace(" ", "%").RemoveAccent()}%";
            return query;
        }

        public static string FirstCharToUpper(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}