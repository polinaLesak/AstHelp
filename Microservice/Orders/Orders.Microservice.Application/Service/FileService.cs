using Microsoft.AspNetCore.Hosting;

namespace Orders.Microservice.Application.Service
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
    }
}
