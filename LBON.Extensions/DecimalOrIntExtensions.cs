using System.ComponentModel;
using System.Text.RegularExpressions;

namespace LBON.Extensions
{
    public static class DecimalOrIntExtensions
    {
        /// <summary>Converts to chinese amount.</summary>
        /// <param name="number">The number.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("换算成中文金额")]
        public static string ToChineseAmount(this decimal number)
        {
            return BuildChineseAmount(number);
        }

        /// <summary>Converts to chinese amount.</summary>
        /// <param name="number">The number.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string ToChineseAmount(this int number)
        {
            return BuildChineseAmount(number);
        }

        /// <summary>Determines whether the specified minimum is between.</summary>
        /// <param name="number">The number.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>
        ///   <c>true</c> if the specified minimum is between; otherwise, <c>false</c>.</returns>
        [Description("判断值是否介于两者之间")]
        public static bool IsBetween(this decimal number, decimal min, decimal max)
        {
            return number >= min && number <= max;
        }

        /// <summary>Determines whether the specified minimum is between.</summary>
        /// <param name="number">The number.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>
        ///   <c>true</c> if the specified minimum is between; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(this int number, int min, int max)
        {
            return number >= min && number <= max;
        }

        /// <summary>Determines whether the specified minimum is between.</summary>
        /// <param name="number">The number.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>
        ///   <c>true</c> if the specified minimum is between; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(this float number, float min, float max)
        {
            return number >= min && number <= max;
        }

        /// <summary>Determines whether the specified minimum is between.</summary>
        /// <param name="number">The number.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>
        ///   <c>true</c> if the specified minimum is between; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(this double number, double min, double max)
        {
            return number >= min && number <= max;
        }

        /// <summary>Determines whether the specified minimum is between.</summary>
        /// <param name="number">The number.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>
        ///   <c>true</c> if the specified minimum is between; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(this decimal? number, decimal min, decimal max)
        {
            return number.HasValue && (number >= min && number <= max);
        }

        /// <summary>Determines whether the specified minimum is between.</summary>
        /// <param name="number">The number.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>
        ///   <c>true</c> if the specified minimum is between; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(this int? number, int min, int max)
        {
            return number.HasValue && (number >= min && number <= max);
        }

        /// <summary>Determines whether the specified minimum is between.</summary>
        /// <param name="number">The number.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>
        ///   <c>true</c> if the specified minimum is between; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(this float? number, float min, float max)
        {
            return number.HasValue && (number >= min && number <= max);
        }

        /// <summary>Determines whether the specified minimum is between.</summary>
        /// <param name="number">The number.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>
        ///   <c>true</c> if the specified minimum is between; otherwise, <c>false</c>.</returns>
        public static bool IsBetween(this double? number, double min, double max)
        {
            return number.HasValue && (number >= min && number <= max);
        }

        private static string BuildChineseAmount(decimal number)
        {
            var s = number.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var d = Regex.Replace(s,
                @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))",
                "${b}${z}");
            var r = Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return r;
        }
    }
}