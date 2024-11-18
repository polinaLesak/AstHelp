namespace Catalog.Microservice.Domain.Models.Sorting
{
    public class SortingRequest
    {
        public string Field { get; set; } = "Id";
        public SortDirection Direction { get; set; } = SortDirection.Asc;
    }
}
