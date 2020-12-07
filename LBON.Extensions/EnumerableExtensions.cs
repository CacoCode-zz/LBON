using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBON.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Joins the specified separator.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }
    }
}
