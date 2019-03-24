using System;
using System.Threading;
using Amazon.Runtime.Internal.Util;

namespace Ataraxia.Tools.Storage.S3
{
    public class FileOptions
    {
        public string Directory { get; set; }
        public bool Compress { get; set; }
    }
}
