using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using Myrmec;
using SimpleFormsService.API.Configs;

namespace SimpleFormsService.API.Services.Impl
{
    public class MinIoFileStorageService : IDocumentService
    {
        private readonly OpenshiftConfig _openshiftConfig;
        public MinioClient client;
        

        public MinIoFileStorageService(OpenshiftConfig openshiftConfig)
        {
            _openshiftConfig = openshiftConfig;
            client = new MinioClient(_openshiftConfig.MINIO_EndPoint, _openshiftConfig.MINIO_AccessKey, _openshiftConfig.MINIO_SecretKey);
        }

        /// <summary>
        /// Check whether the object exists. If it does, return the object
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public async Task<ObjectStat> FindObject(string bucketName, string objectName)
        {          
            ObjectStat objectStat;
            objectStat = await client.StatObjectAsync(bucketName, objectName);
            Console.WriteLine("===== Object Stat: " + objectStat + " ======");
            return objectStat;
        }

        /// <summary>
        /// Get object from bucket and write it in to memory
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public async Task<MemoryStream> GetObject(string bucketName,string objectName)
        {
            var responseStream = new MemoryStream();

            try
            {
                await client.GetObjectAsync(bucketName, objectName, (stream) =>
                {
                    stream.CopyTo(responseStream);
                    responseStream.Position = 0;
                    stream.Dispose();
                });
            }
            catch(MinioException ex)
            {
                Console.WriteLine("===== ERROR: unable to get object - " + objectName + " =====" + ex);
            }
            return responseStream;
        }

        public async Task<string> UploadFiles(List<IFormFile> files, List<string> objectNames, string bucketName)
        {
            long size = 0;
            string fileName = "";
            string objectName = "";

            try
            {
                //check form collection and process files
                if (files != null && files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        IFormFile file = files[i];
                                         
                        if (file != null)
                        {
                            size = file.Length;
                            fileName = file.FileName;
                        }
                        //TODO remove duplicates
                        //try
                        //{
                        //    if (objectNames != null && objectNames.Count > 0)
                        //    { 
                        //        foreach(string name in objectNames)
                        //        {
                                    
                        //        }
                        //    ///need to replace existing upload
                        //    await client.RemoveObjectAsync(bucketName, formCollection[key].ToString());
                        //    Console.WriteLine($"Need to replace existing upload. Deleting object {formCollection[key].ToString()} from bucket {BUCKET_NAME} successfully");
                        //    }
                        //}
                        //catch (Exception e)
                        //{
                        //    Console.WriteLine($"ERROR: Unable to delete existing upload: {e}");
                        //}
                                         
                        if (size > 0)
                        {
                            Console.WriteLine("========= Size check done and ready to upload at " + DateTime.UtcNow + " ==========");
                            ///Making sure file type matches content type
                            List<string> results = MatchFileWithContent(file);
                            Console.WriteLine("========= Snif check passed at " + DateTime.UtcNow + " ========");

                            if (results.Count > 0)
                            {
                                using var memoryStream = new MemoryStream();
                                await file.CopyToAsync(memoryStream);
                                memoryStream.Position = 0;

                                objectName = await Upload(bucketName, memoryStream, fileName, file.ContentType);
                                file = null;
                            }
                        }                 
                    }//end of for
                }//end of if
            }//end of try
            catch (Exception ex)
            {
                Console.WriteLine("===== ERROR: Unable to upload files =====" + ex.StackTrace);
                throw;
            }
            return objectName;
        }

        //TODO
        //public Task RemoveObjects(List<IFormFile> files, List<string> objectNames)
        //{
        //    return;
        //}

        /// <summary>
        /// Making sure file extension matches file content provided.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected List<string> MatchFileWithContent(IFormFile file)
        {
            var sniffer = new Sniffer();
            var supportedFiles = new List<Record>
                {
                    new Record("png", "89,50,4e,47,0d,0a,1a,0a"),
                    new Record("pdf", "25 50 44 46"),
                    new Record("jpg,jpeg", "ff,d8,ff,db"),
                    new Record("jpg,jpeg","FF D8 FF E0 ?? ?? 4A 46 49 46 00 01"),
                    new Record("jpg,jpeg","FF D8 FF E1 ?? ?? 45 78 69 66 00 00")
                };
            sniffer.Populate(supportedFiles);
            ///making sure file type matches content type
            byte[] fileHead = ReadFileHead(file);
            var results = sniffer.Match(fileHead);
            return results;
        }

        protected byte[] ReadFileHead(IFormFile file)
        {
            using var fs = new BinaryReader(file.OpenReadStream());
            var bytes = new byte[20];
            fs.Read(bytes, 0, 20);
            return bytes;
        }

        protected async Task<string> Upload(string bucketName, Stream fileStream, string fileName, string contentType)
        {
            ///file name in the bucket
            string objectName = Guid.NewGuid().ToString().ToLower();

            try
            {
                // Make a bucket on the server, if not already present.
                bool found = await client.BucketExistsAsync(bucketName);
                if (!found)
                    await client.MakeBucketAsync(bucketName);

                Console.WriteLine("======== New bucket created: " + bucketName + " ==========");

                Dictionary<string, string> metaData = new Dictionary<string, string>
                {
                    { "Metadata-Filename", fileName }
                };
                // Upload a file to bucket.
                await client.PutObjectAsync(bucketName, objectName, fileStream, fileStream.Length, contentType, metaData: metaData);

                Console.WriteLine("========= Successfully uploaded " + objectName + " ============");
            }
            catch (Exception e)
            {
                Console.WriteLine("========== File upload ERROR: {0} =======", e.Message);
            }
            return objectName;
        }
    }
}
