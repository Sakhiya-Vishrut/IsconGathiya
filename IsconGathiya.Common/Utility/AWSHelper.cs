using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IsconGathiya.Common.Utility
{
    public class AWSHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        private static readonly RegionEndpoint dev_bucketRegion = RegionEndpoint.USEast1;
        private static readonly RegionEndpoint production_bucketRegion = RegionEndpoint.USEast1;

        private static string dev_bucketName = "IsconGathiya-dev";
        private static string production_bucketName = "IsconGathiya-production";
        private static string bucketName;

        private static string dev_awsAccessKeyId = "AKIAW3MEEYFH53ORSMXQ";
        private static string production_awsAccessKeyId = "AKIAW3MEEYFHW44RHWMR";

        private static string dev_awsSecretAccessKey = "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";
        private static string production_awsSecretAccessKey = "aTc8/EFAyFfaGXLOjaZKpIglFy3BBtUb2GYRIzRW";

        private static IAmazonS3 s3Client;

        public static void Initialize(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private static void Set_s3Client()
        {
            if (_httpContextAccessor.HttpContext?.Request.Host.ToUriComponent() != "uni-admin.ahmedabadmedia.in")
            {
                s3Client = new AmazonS3Client(dev_awsAccessKeyId, dev_awsSecretAccessKey, dev_bucketRegion);
                bucketName = dev_bucketName;
            }
            else
            {
                s3Client = new AmazonS3Client(production_awsAccessKeyId, production_awsSecretAccessKey, production_bucketRegion);
                bucketName = production_bucketName;
            }
            // s3Client = new AmazonS3Client(bucketRegion);
        }

        public static string CorrectPath(string path)
        {
            path = path.Replace("%23", "#");
            if (!String.IsNullOrEmpty(path) && path.Contains("\\"))
                path = path.Replace("\\", "/");

            return path;
        }

        public static async Task UploadFileToS3(byte[] fileBytes, string fileName)
        {
            try
            {

                Set_s3Client();
                using (var memoryStream = new MemoryStream(fileBytes))
                {
                    var putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = fileName,
                        InputStream = memoryStream
                    };

                    PutObjectResponse response = await s3Client.PutObjectAsync(putRequest);
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when writing an object");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown error encountered on server. Message:'{e.Message}' when writing an object");
            }
        }

        public async Task UploadFileAsync(string bucketName, string keyName, string content)
        {
            try
            {
                Set_s3Client();
                // Create a temporary file
                string tempFilePath = Path.GetTempFileName();
                await File.WriteAllTextAsync(tempFilePath, content);

                // Upload file to S3
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = tempFilePath,
                    ContentType = "text/plain"
                };

                PutObjectResponse response = await s3Client.PutObjectAsync(putRequest);

                Console.WriteLine($"File uploaded successfully to {bucketName}/{keyName}");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when writing an object");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when writing an object");
            }
        }

        public static async Task<string> ReadFileAsync(string key)
        {
            try
            {
                Set_s3Client();

                // Create a GetObjectRequest
                var getRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = CorrectPath(key)
                };

                // Read the file from S3
                using (GetObjectResponse response = await s3Client.GetObjectAsync(getRequest))
                using (StreamReader reader = new StreamReader(response.ResponseStream))
                {
                    string jsonString = await reader.ReadToEndAsync();
                    Console.WriteLine("File read successfully from S3");
                    return jsonString;
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when reading an object");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when reading an object");
                return null;
            }
        }

        public static async Task UploadFileAsync(string filePath, string content)
        {
            try
            {
                Set_s3Client();
                // Create a MemoryStream from the content
                using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
                {
                    // Create a PutObjectRequest
                    var putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = CorrectPath(filePath),
                        InputStream = memoryStream,
                        ContentType = "text/plain"
                    };

                    // Upload the file to S3
                    PutObjectResponse response = await s3Client.PutObjectAsync(putRequest);

                    Console.WriteLine($"File uploaded successfully to {bucketName}/{filePath}");
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when writing an object");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when writing an object");
            }
        }

        public static async Task<string> UploadFileToAWS(Stream file, string filePath)
        {
            var path = "";
            try
            {
                Set_s3Client();
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = CorrectPath(filePath),
                    InputStream = file,
                };

                await s3Client.PutObjectAsync(request);
                path = "https://IsconGathiya-dev.s3.amazonaws.com/" + IsconGathiya.Common.Utility.AWSHelper.CorrectPath(filePath);
                if (_httpContextAccessor.HttpContext?.Request.Host.ToUriComponent() == "uni-admin.ahmedabadmedia.in")
                {
                    path = "https://pdsahmdabad.s3.ap-south-1.amazonaws.com/" + IsconGathiya.Common.Utility.AWSHelper.CorrectPath(filePath);
                }
                return path;
            }
            catch (AmazonS3Exception ex)
            {
                // Log AWS-specific exception
                Console.WriteLine($"Error encountered on server. Message:'{ex.Message}' when writing an object");
            }
            catch (Exception ex)
            {
                // Log general exception
                Console.WriteLine($"Unknown error encountered: {ex.Message}");
            }
            return path;
        }

        public static string GetFileFromAWS(string path)
        {
            try
            {
                string generatedPath = "https://" + bucketName + ".s3-us-east-1.amazonaws.com/" + path;
                return generatedPath;
            }
            catch (Exception ex)
            {
                // _logger.LogError("Error in GetFileFromAWS", ex);
                return null;
            }
        }

        public static async Task<byte[]> DownloadFileFromAws(string file)
        {
            MemoryStream ms = null;
            try
            {
                Set_s3Client();
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = CorrectPath(file)
                };

                using (var response = await s3Client.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", file));

                return ms.ToArray();
            }
            catch (Exception ex)
            {
                // _logger.LogError("Error in DownloadFileFromAws", ex);
                return null;
            }
        }

        public static async Task DeleteFileAsync(string key)
        {
            try
            {
                Set_s3Client();

                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = CorrectPath(key)
                };

                DeleteObjectResponse response = await s3Client.DeleteObjectAsync(deleteRequest);
                Console.WriteLine($"File deleted successfully from {bucketName}/{key}");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when deleting an object");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when deleting an object");
            }
        }

        public static async Task<byte[]> GetObject(string file)
        {
            MemoryStream ms = null;
            try
            {
                Set_s3Client();
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = CorrectPath(file)
                };

                using (var response = await s3Client.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", file));

                return ms.ToArray();
            }
            catch (Exception ex)
            {
                // _logger.LogError("Error in GetObject", ex);
                throw ex;
            }
        }
    }
}
