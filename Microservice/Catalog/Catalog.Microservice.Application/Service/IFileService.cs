using Microsoft.AspNetCore.Http;

namespace Catalog.Microservice.Application.Service
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderPath);
        void DeleteFile(string filePath);
    }
}
