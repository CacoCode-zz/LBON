using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LBON.Consts;

namespace LBON.Extensions
{
    public static class StringExtensions
    {
        /// <summary>Determines whether [is null or white space].</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is null or white space] [the specified string]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>Determines whether [is null or empty].</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified string]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>Copies the specified string.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string Copy(this string str)
        {
            return string.Copy(str);
        }

        /// <summary>Gets the phone number.</summary>
        /// <param name="str">The string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string GetPhoneNumber(this string str, string pattern = RegexConst.PhoneNumber)
        {
            var reg = new Regex(pattern);
            var match = reg.Match(str);
            if (match.Success)
            {
                return match.Value;
            }
            return null;
        }

        /// <summary>Gets the phone numbers.</summary>
        /// <param name="str">The string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static List<string> GetPhoneNumbers(this string str, string pattern = RegexConst.PhoneNumber)
        {
            var reg = new Regex(pattern);
            var matches = reg.Matches(str);
            return (from Match item in matches select item.Value).ToList();
        }

        /// <summary>Regex Match the specified pattern.</summary>
        /// <param name="str">The string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string RegexMatch(this string str, string pattern)
        {
            var reg = new Regex(pattern);
            var match = reg.Match(str);
            if (match.Success)
            {
                return match.Value;
            }
            return null;
        }

        /// <summary>Regex Matches the specified pattern.</summary>
        /// <param name="str">The string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static List<string> RegexMatches(this string str, string pattern)
        {
            var reg = new Regex(pattern);
            var matches = reg.Matches(str);
            return (from Match item in matches select item.Value).ToList();
        }

        /// <summary>Converts the string representation of a number to an integer.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        /// <summary>Converts to decimal.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static decimal ToDecimal(this string str)
        {
            return decimal.Parse(str);
        }

        /// <summary>Determines whether this instance is numeric.</summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is numeric; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(this string str)
        {
            var regex = new Regex(RegexConst.IsNumeric);
            return regex.IsMatch(str);
        }
    }
}
