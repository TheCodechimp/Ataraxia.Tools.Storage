using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Ataraxia.Tools.Storage.Extensions;
using Ataraxia.Tools.Storage.S3;
using Ataraxia.Tools.Testing;
using AutoFixture.Xunit2;
using Moq;
using Shouldly;
using Xunit;

namespace Ataraxia.Tools.Storage.Tests
{
    public class S3StorageManagerTests
    {
        [Theory]
        [AutoMock]
        public async Task WhenReadingAnAsyncFile_ShouldReturnStream(
            [Frozen] Mock<IAmazonS3> client,
            S3StorageManager sut)
        {            
            client.Setup(r => r.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetObjectResponse()
            {
                BucketName = "Test",
                ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes("I AM A TEST!"))
            });

            var result = await sut.ReadAsync("foo");
            result.ShouldSatisfyAllConditions(() =>
            {
                result.ShouldBeOfType<MemoryStream>();
                result.Length.ShouldBeGreaterThan(0);
                result.ConvertToString().ShouldBe("I AM A TEST!");
            });
        }
    }
}