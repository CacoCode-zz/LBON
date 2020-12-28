using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LBON.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Gets all bytes.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        [Description("获取字节数组")]
        public static byte[] GetAllBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Gets all bytes asynchronous.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static async Task<byte[]> GetAllBytesAsync(this Stream stream, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.Position = 0;
                await stream.CopyToAsync(memoryStream, cancellationToken);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Copies to asynchronous.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [Description("复制")]
        public static Task CopyToAsync(this Stream stream, Stream destination, CancellationToken cancellationToken)
        {
            stream.Position = 0;
            return stream.CopyToAsync(
                destination,
                81920, //this is already the default value, but needed to set to be able to pass the cancellationToken
                cancellationToken
            );
        }
    }
}
