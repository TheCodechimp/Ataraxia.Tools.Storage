using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Atraxia.Tools.Storage;
using Microsoft.Extensions.Logging;

namespace Ataraxia.Tools.Storage.S3
{

    public class S3StorageManager : IManageFiles
    {
        private readonly ILogger<S3StorageManager> _logger;
        private readonly IAmazonS3 _client;
        private readonly FileOptions _options;

        public S3StorageManager(ILogger<S3StorageManager> logger, IAmazonS3 client, FileOptions options)
        {
            _logger = logger;
            _client = client;
            _options = options;
        }

        public async Task<Stream> ReadAsync(string fileName)
        {
            GetObjectRequest request = new GetObjectRequest();
            request.BucketName = _options.Directory;
            request.Key = fileName;
            string responseBody = "";
            var memoryStream = new MemoryStream();

            using (GetObjectResponse response = await _client.GetObjectAsync(request))
            using (Stream responseStream = response.ResponseStream)
                await responseStream.CopyToAsync(memoryStream);

            return memoryStream;
        }

        private MemoryStream EncodeString(string content)
        {
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            memoryStream.Flush();
            memoryStream.Position = 0;
            return memoryStream;
        }

        private MemoryStream CompressString(string content)
        {
            var memoryStream = new MemoryStream();

            using (var zip = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                var buffer = Encoding.UTF8.GetBytes(content);
                zip.Write(buffer, 0, buffer.Length);
                zip.Flush();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;
            return memoryStream;
        }

    }
}