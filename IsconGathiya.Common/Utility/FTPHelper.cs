using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace IsconGathiya.Common.Utility
{
    public class FTPHelper
    {
        public static string ftpServer = "168.231.120.235";
        public static string ftpUsername = "vinayakftp";
        public static string ftpPassword = "VinayaK@#4523!";
        private static void Set_FTPClient()
        {

            // s3Client = new AmazonS3Client(production_awsAccessKeyId, production_awsSecretAccessKey, production_bucketRegion);
            // bucketName = production_bucketName;
            // s3Client = new AmazonS3Client(bucketRegion);
        }
        public static string SanitizeFileName(string fileName)
        {
            return Path.GetFileName(fileName)
                       .Replace(" ", "_") // Optional: replace spaces
                       .Replace("\"", "") // remove double quotes
                       .Replace("'", "")
                       .Replace(":", "")
                       .Replace("?", "")
                       .Replace("<", "")
                       .Replace(">", "")
                       .Replace("|", "")
                       .Replace("*", "");
        }
        public static async Task<string> UploadFileToFTP(Stream file, string filePath)
        {
            try
            {
                string url = $"ftp://{ftpServer}/vinayakStorage/{filePath}";
                string ftpurl = $"https://eembranding.com/blog-images/{filePath}";
                Console.WriteLine($"Uploading to: {url}");

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                request.UsePassive = true;
                request.KeepAlive = false;

                using (Stream ftpStream = request.GetRequestStream())
                {
                    file.CopyTo(ftpStream);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"FTP Response: {response.StatusDescription}");
                }

                return filePath; // or return only relative path if you prefer
            }
            catch (WebException ex)
            {
                Console.WriteLine($"FTP Error: {ex.Message}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unknown error: {ex.Message}");
                return string.Empty;
            }
        }


        public static void EnsureDirectoryExists(string folderPath)
        {
            string[] folders = folderPath.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            string currentPath = $"ftp://{ftpServer}/vinayakStorage";

            foreach (var folder in folders)
            {
                currentPath += "/" + folder;

                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(currentPath);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    request.UsePassive = true;
                    request.KeepAlive = false;

                    using (var resp = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine($"Folder created: {resp.StatusDescription}");
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response is FtpWebResponse response &&
                        response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        Console.WriteLine($"Folder already exists: {currentPath}");
                    }
                    else
                    {
                        Console.WriteLine($"Error creating folder {folder}: {ex.Message}");
                    }
                }
            }
        }
        public static async Task<string> UploadFileToFTPHelper(string existingFilePath, string fileName, string filePath, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string extension = Path.GetExtension(file.FileName);
                    string formattedFileName = CommonHelper.FormatFileName(file.FileName).Split(".").First();
                    string uniqueFileName = $"{formattedFileName}-{Guid.NewGuid()}{extension}";

                    // Create folder if not exists
                    EnsureDirectoryExists(filePath);

                    string fullFtpPath = Path.Combine(filePath, uniqueFileName).Replace("\\", "/");

                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        memoryStream.Position = 0;

                        return await UploadFileToFTP(memoryStream, fullFtpPath);
                    }
                }

                return existingFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error uploading file: " + ex.Message);
                return string.Empty;
            }
        }
    }
}
