using System.IO;

namespace Structum.Elements.Extensions
{
    /// <summary>
    ///     Defines the Stream Extensions.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        ///     Converts Stream into a Memory Stream.
        /// </summary>
        /// <remarks>
        ///     This method creates an open MemoryStream with a starting position to 0. Don't forget to dispose it when it is no longer needed.
        /// </remarks>
        /// <param name="stream">Stream to Convert.</param>
        /// <returns><c>MemoryStream</c></returns>
        public static MemoryStream ToMemoryStream(Stream stream)
        {
            MemoryStream memStream = new MemoryStream();
            const int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int count;

            while((count = stream.Read(buffer, 0, bufferLen)) > 0) {
                memStream.Write(buffer, 0, count);
            }

            memStream.Position = 0;

            return memStream;
        }
    }
}
