using System;
using System.IO;
using System.Threading.Tasks;

namespace Atraxia.Tools.Storage
{
    public interface IManageFiles
    {
        Task<Stream> ReadAsync(string fileName);        
    }
}