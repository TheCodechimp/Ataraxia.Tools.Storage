using System;
using System.IO;
using System.Threading.Tasks;
using Ataraxia.Tools.Storage.S3;
using Ataraxia.Tools.Testing;
using Shouldly;
using Xunit;

namespace Ataraxia.Tools.Storage.Tests
{
    public class S3StorageManagerTests
    {
        [Theory]
        [AutoMock]
        public async Task WhenReadingAnAsyncFile_ShouldReturnStream(S3StorageManager sut)
        {

            var result = await sut.ReadAsync("TestFile");
            result.ShouldSatisfyAllConditions(() =>
            {
                result.ShouldBeOfType<Stream>();
                result.Length.ShouldBeGreaterThan(0);
            });
        }
    }
}
