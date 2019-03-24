using System.IO;

namespace Atraxia.Tools.Storage
{
    public class FileMetaData
    {
        public string FileName { get; set; }
        public Stream Contents { get; set; }
        public string ContentType { get; set; }
    }
}