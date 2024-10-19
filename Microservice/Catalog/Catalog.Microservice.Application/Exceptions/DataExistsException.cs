namespace Catalog.Microservice.Application.Exceptions
{
    public class DataExistsException : Exception
    {
        public DataExistsException(string message)
            : base(message) { }
    }
}
