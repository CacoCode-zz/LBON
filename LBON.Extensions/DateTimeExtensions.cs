using System;
using System.Collections.Generic;
using System.Text;

namespace LBON.Extensions
{
    public static class DateTimeExtensions
    {

        /// <summary>
        /// Firsts the of month.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <returns></returns>
        public static DateTime FirstOfMonth(this DateTime current)
        {
            var first = current.AddDays(1 - current.Day);
            return first;
        }

        /// <summary>
        /// Lasts the of month.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <returns></returns>
        public static DateTime LastOfMonth(this DateTime current)
        {
            var daysInMonth = DateTime.DaysInMonth(current.Year, current.Month);
            var last = current.FirstOfMonth().AddDays(daysInMonth - 1);
            return last;
        }
    }
}
