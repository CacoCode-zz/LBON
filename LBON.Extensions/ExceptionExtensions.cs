using System;
using System.ComponentModel;
using System.Runtime.ExceptionServices;

namespace LBON.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Exception"/> class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Uses <see cref="ExceptionDispatchInfo.Capture"/> method to re-throws exception
        /// while preserving stack trace.
        /// </summary>
        /// <param name="exception">Exception to be re-thrown</param>
        [Description("重新引发异常")]
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}