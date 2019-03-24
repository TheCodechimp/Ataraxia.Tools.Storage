using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Atraxia.Tools.Storage
{
    public interface IManageFiles
    {
        Task<Stream> ReadAsync(string fileName);
        Task WriteAsync(FileMetaData fileInfo);
        Task<IEnumerable<string>> ListFilesAsync();
    }
}