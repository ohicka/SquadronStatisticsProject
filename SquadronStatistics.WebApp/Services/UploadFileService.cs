using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SquadronStatistics.WebApp.Services
{
    public class UploadFileService: IUploadFileService
    {
        private readonly string _uploadedFilesFolder;

        public UploadFileService(IConfiguration config)
        {
            _uploadedFilesFolder = config.GetSection("FileUpload:UploadedFilesFolder").Value;
        }

        public async Task<string> SaveFileToLocalDirectory(IFormFile file, string fileName)
        {
            try
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), _uploadedFilesFolder);
                var filePath = Path.Combine(directory, fileName);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(fs);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                // log error
                return null;
            }
            
        }
    }
}
