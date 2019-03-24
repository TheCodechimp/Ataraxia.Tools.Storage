using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Ataraxia.Tools.Storage.Extensions;
using Ataraxia.Tools.Storage.S3;
using Ataraxia.Tools.Testing;
using Atraxia.Tools.Storage;
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

        [Theory]
        [AutoMock]
        public async Task WhenWritingAnAsyncFile_ShouldSucceed(
            FileMetaData fileInfo,            
            S3StorageManager sut)
        {
            Should.NotThrow(async () => { await sut.WriteAsync(fileInfo); });
        }

        [Theory]
        [AutoMock]
        public async Task WhenGettingBucketListings_ShouldReturnFileResults(
            [Frozen] Mock<IAmazonS3> client,            
            S3StorageManager sut)
        {
            client.Setup(r => r.ListObjectsAsync(It.IsAny<ListObjectsRequest>(), CancellationToken.None))
                .ReturnsAsync(new ListObjectsResponse()
                {
                    S3Objects = new List<S3Object>()
                    {
                        new S3Object()
                        {
                            BucketName = "TestBucket",
                            Key = "TestFile"                            
                        }
                    }
                });

            var result = await sut.ListFilesAsync();
            result.ShouldSatisfyAllConditions(() =>
            {
                result.ShouldBeOfType<List<string>>();
                result.Count().ShouldBe(1);
                result.First().ShouldBe("TestFile");
            });

        }

    }
}