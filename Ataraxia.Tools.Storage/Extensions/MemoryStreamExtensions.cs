using System.IO;

namespace Ataraxia.Tools.Storage.Extensions
{
    public static class MemoryStreamExtensions
    {
        public static string ConvertToString(this MemoryStream stream)
        {
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public static class StreamExtensions
    {
        public static string ConvertToString(this Stream stream)
        {
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

}
