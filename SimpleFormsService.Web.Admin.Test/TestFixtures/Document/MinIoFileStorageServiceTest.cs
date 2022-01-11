using System.IO;
using System.Threading.Tasks;
using Minio.DataModel;
using Xunit;

namespace SimpleFormsService.Test.TestFixtures.Document
{
    public class MinIoFileStorageServiceTest
    {
        private readonly DocumentServiceMockFactory _mockFactory;
        public MinIoFileStorageServiceTest()
        {
            _mockFactory = new DocumentServiceMockFactory();
        }

        [Fact]
        public async Task GetObjectReturnAResultAsync()
        {
            var responseStream = await _mockFactory.GetObject(_mockFactory.bucketName, _mockFactory.objectName);
            MemoryStream ms = new MemoryStream();
            Assert.True(responseStream.Capacity > 0);
        }

        [Fact]
        public async Task FindObjectReturnAResultAsync()
        {
            ObjectStat objStat = await _mockFactory.FindObject(_mockFactory.bucketName, _mockFactory.objectName);
            Assert.NotNull(objStat);
        }

        [Fact]
        public async Task RemoveFileReturnTrueAsync()
        {
            bool status = await _mockFactory.RemoveFile(_mockFactory.bucketName, _mockFactory.objectName);
            Assert.True(status == true);
        }
    }
}
