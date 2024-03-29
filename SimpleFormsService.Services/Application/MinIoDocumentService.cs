﻿using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using Myrmec;
using SimpleFormsService.Domain.Entities.Supporting;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Services.Abstractions.Application;

namespace SimpleFormsService.Services.Application
{
    public class MinioDocumentService : ServiceBase, IDocumentService
    {
        public MinioClient _client;
        private readonly IFormTemplateSecurityService _formTemplateSecurityService;
        
        public MinioDocumentService(MinioClient client, IFormTemplateSecurityService formTemplateSecurityService)
        {
            _client = client;
            _formTemplateSecurityService = formTemplateSecurityService;
        }
       
        /// <summary>
        /// Get object from bucket 
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<FileStreamResultAdapter> GetObject(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(bucketName, nameof(bucketName));
            Guard.AgainstInvalidGuidFormat(bucketName, nameof(bucketName));
            Guard.AgainstNullEmptyOrWhiteSpace(objectName, nameof(objectName));
            Guard.AgainstInvalidGuidFormat(objectName, nameof(objectName));

            var isUserAuthorized = await _formTemplateSecurityService.IsUserAuthorized(bucketName, cancellationToken);

            if (isUserAuthorized)
            {
                var responseStream = new MemoryStream();

                try
                {
                    await _client.GetObjectAsync(bucketName, objectName, (stream) =>
                    {
                        stream.CopyTo(responseStream);
                        responseStream.Position = 0;
                        stream.Dispose();
                    }, cancellationToken: cancellationToken);
                }
                catch (MinioException ex)
                {
                    Console.WriteLine("===== ERROR: unable to get object - " + objectName + " =====" + ex);
                }

                var objectStat = await FindObject(bucketName, objectName, cancellationToken);

                return new FileStreamResultAdapter(objectStat.ContentType, responseStream);
            }

            throw new NotAuthorizedException("form template", bucketName);
        }

        /// <summary>
        /// This method add a list of files to a minio bucket.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="files"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<string>> UploadFiles(string bucketName, List<IFormFile> files, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(bucketName, nameof(bucketName));
            Guard.AgainstInvalidGuidFormat(bucketName, nameof(bucketName));
            Guard.AgainstNullOrEmptyList(files, nameof(files));

            var objectNames = new List<string>();

            try
            {
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        var size = file.Length;

                        #region // todo remove duplicates 
                        
                        //try
                        //{
                        //    if (objectNames != null && objectNames.Count > 0)
                        //    { 
                        //        foreach(string name in objectNames)
                        //        {

                        //        }
                        //    ///need to replace existing upload
                        //    await _client.RemoveObjectAsync(bucketName, formCollection[key].ToString());
                        //    Console.WriteLine($"Need to replace existing upload. Deleting object {formCollection[key].ToString()} from bucket {BUCKET_NAME} successfully");
                        //    }
                        //}
                        //catch (Exception e)
                        //{
                        //    Console.WriteLine($"ERROR: Unable to delete existing upload: {e}");
                        //}

                        #endregion

                        if (size > 0)
                        {
                            var results = MatchFileWithContent(file);

                            if (results.Count > 0)
                            {
                                Console.WriteLine("====== INFO: Ready to upload object ======");
                                var objectName = await UploadFile(file, bucketName: bucketName, cancellationToken);
                                objectNames.Add(objectName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            return objectNames;
        }
        
        /// <summary>
        /// This method removes an object from minio bucket.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> RemoveFile(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(bucketName, nameof(bucketName));
            Guard.AgainstInvalidGuidFormat(bucketName, nameof(bucketName));
            Guard.AgainstNullEmptyOrWhiteSpace(objectName, nameof(objectName));
            Guard.AgainstInvalidGuidFormat(objectName, nameof(objectName));
            
            var status = false;
            try
            {
                await _client.RemoveObjectAsync(bucketName, objectName, cancellationToken);
                status = true;
                Console.WriteLine($"Removed object {objectName} from bucket {bucketName} successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: [Bucket-Object] Exception: {e}");
            }
            return status;
        }

        #region Private Helpers

       /// <summary>
       /// Check whether the object exists. If it does, return the object
       /// </summary>
       /// <param name="bucketName"></param>
       /// <param name="objectName"></param>
       /// <param name="cancellationToken"></param>
       /// <returns></returns>
       private async Task<ObjectStat> FindObject(string bucketName, string objectName, CancellationToken cancellationToken = default)
       {
           var objectStat = await _client.StatObjectAsync(bucketName, objectName, cancellationToken: cancellationToken);
           Console.WriteLine("===== Object Stat: " + objectStat + " ======");
           return objectStat;
       }

        private static List<string> MatchFileWithContent(IFormFile file)
        {
            var sniffer = new Sniffer();

            var supportedFiles = new List<Record>
            {
                new Record("png", "89,50,4e,47,0d,0a,1a,0a"),
                new Record("pdf", "25 50 44 46"),
                new Record("jpg,jpeg", "ff,d8,ff,db"),
                new Record("jpg,jpeg","FF D8 FF E0 ?? ?? 4A 46 49 46 00 01"),
                new Record("jpg,jpeg","FF D8 FF E1 ?? ?? 45 78 69 66 00 00"),
                new Record("docx", "50,4b,03,04"),
                new Record("docx", "50,4b,07,08"),
                new Record("docx", "50,4b,05,06"),
                new Record("doc", "D0 CF 11 E0 A1 B1 1A E1"),
            };

            sniffer.Populate(supportedFiles);
            
            var fileHead = ReadFileHead(file);
            
            var results = sniffer.Match(fileHead); 
            
            return results;
        }

        private static byte[] ReadFileHead(IFormFile file)
        {
            using var fs = new BinaryReader(file.OpenReadStream());
            var bytes = new byte[20];
            fs.Read(bytes, 0, 20);
            return bytes;
        }
        
        private async Task<string> UploadFile(IFormFile file, string bucketName, CancellationToken cancellationToken = default)
        {
            var objectName = Guid.NewGuid().ToString().ToLower();

            try
            {
                var found = await _client.BucketExistsAsync(bucketName, cancellationToken);
                Console.WriteLine("==== INFO: Bucket " + bucketName + " found? " + found + " ====");
                if (!found)
                {
                    await _client.MakeBucketAsync(bucketName, cancellationToken: cancellationToken);
                }

                var metaData = new Dictionary<string, string>
                {
                    { "Metadata-Filename", file.FileName }
                };

                await _client.PutObjectAsync(bucketName, objectName, file.OpenReadStream(), file.Length, file.ContentType, cancellationToken: cancellationToken, metaData: metaData);
                Console.WriteLine("Successfully uploaded " + file.FileName);
            }
            catch (MinioException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);
            }

            return objectName;
        }

        #endregion
    }
}
