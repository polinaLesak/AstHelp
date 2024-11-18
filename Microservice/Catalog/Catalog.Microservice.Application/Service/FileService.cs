using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Catalog.Microservice.Application.Service
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Stream GetTemplateStream(string templateName)
        {
            var templatePath = Path.Combine(_environment.WebRootPath, "Templates", templateName);
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException("Шаблон документа не найден", templateName);
            }

            return new FileStream(templatePath, FileMode.Open, FileAccess.Read);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(_environment.WebRootPath, folderPath);
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/{folderPath}/{uniqueFileName}";
        }
    }
}
