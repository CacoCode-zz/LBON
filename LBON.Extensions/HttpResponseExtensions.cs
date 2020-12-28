using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.Web;
using System.ComponentModel;

namespace LBON.Extensions
{
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Sets the cookie.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <param name="cookie">The cookie.</param>
        [Description("设置请求头")]
        public static void SetCookie(this HttpResponseHeaders headers, Cookie cookie)
        {
            var cookieBuilder = new StringBuilder(HttpUtility.UrlEncode(cookie.Name) + "=" + HttpUtility.UrlEncode(cookie.Value));
            if (cookie.HttpOnly)
            {
                cookieBuilder.Append("; HttpOnly");
            }

            if (cookie.Secure)
            {
                cookieBuilder.Append("; Secure");
            }

            headers.Add("Set-Cookie", cookieBuilder.ToString());
        }
    }
}
