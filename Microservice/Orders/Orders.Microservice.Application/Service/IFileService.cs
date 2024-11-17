namespace Orders.Microservice.Application.Service
{
    public interface IFileService
    {
        Stream GetTemplateStream(string templateName);
    }
}
