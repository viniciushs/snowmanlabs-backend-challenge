namespace System
{
    using System.Text.RegularExpressions;

    public static class StringExtension
    {
        /// <summary>
        ///     Converts the string to Camel Case.
        /// </summary>
        /// <param name="str">
        ///     The string to be converted.
        /// </param>
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.ToTitleCase();
                str = Regex.Replace(str, @"\s+", string.Empty);
            }

            return str;
        }

        /// <summary>
        ///     Converts the string to Title Case.
        /// </summary>
        /// <param name="str">
        ///     The string to be converted.
        /// </param>
        public static string ToTitleCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                // Eliminates multiples spaces and transforms in a unique one
                str = Regex.Replace(str, @"\s+", " ");
                str = Regex.Replace(str.ToLower(), @"(?<=\b(?:mc|mac)?)[a-zA-Z](?<!'s\b)", m => m.Value.ToUpper());
                str = str.Replace(" Da ", " da ").Replace(" De ", " de ").Replace(" Do ", " do ");
                str = str.Trim();
            }

            return str;
        }

        /// <summary>
        ///     Converts the string to Lower Case.
        /// </summary>
        /// <param name="str">
        ///     The string to be converted.
        /// </param>
        public static string ToLowerCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.ToLower().Trim();
            }

            return str;
        }

        /// <summary>
        ///     Converts the string to Upper Case.
        /// </summary>
        /// <param name="str">
        ///     The string to be converted.
        /// </param>
        public static string ToUpperCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.ToUpper().Trim();
            }

            return str;
        }

        /// <summary>
        ///     Replace all caracters by the value passed as parameter.
        /// </summary>
        /// <param name="str">
        ///     The string to be converted.
        /// </param>
        /// <param name="pattern">
        ///     The pattern regex to be find.
        /// </param>
        /// <param name="replacement">
        ///     The value to be replace when the pattern matchs.
        /// </param>
        /// <returns>
        ///     The string to be converted.
        /// </returns>
        public static string RegexReplace(this string str, string pattern, string replacement)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = Regex.Replace(str, pattern, replacement);
            }

            return str;
        }

        /// <summary>
        ///     Eliminates all non digits caracters.
        /// </summary>
        /// <param name="str">
        ///     The string to be converted.
        /// </param>
        public static string ReplaceNonDigits(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var digitsOnly = new Regex(@"[^\d]");
                str = digitsOnly.Replace(str, string.Empty);
            }

            return str;
        }

        /// <summary>
        ///     Replace all caracters non digits by the value passed as parameter.
        /// </summary>
        /// <param name="str">
        ///     The string to be converted.
        /// </param>
        /// <param name="value">
        ///     The new value  to replace.
        /// </param>
        public static string ReplaceNonDigits(this string str, string value)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var digitsOnly = new Regex(@"[^\d]");
                str = digitsOnly.Replace(str, value);
            }

            return str;
        }
    }
}
