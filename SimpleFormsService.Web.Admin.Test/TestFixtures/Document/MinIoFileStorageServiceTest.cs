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
            var response = await _mockFactory.GetObject(_mockFactory.bucketName, _mockFactory.objectName);
            MemoryStream ms = new MemoryStream();
            Assert.True(response.MemoryStream.Capacity > 0);
        }
        
        [Fact]
        public async Task RemoveFileReturnTrueAsync()
        {
            bool status = await _mockFactory.RemoveFile(_mockFactory.bucketName, _mockFactory.objectName);
            Assert.True(status == true);
        }
    }
}
