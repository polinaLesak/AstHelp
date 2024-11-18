using Catalog.Microservice.Domain.Entities;
using Catalog.Microservice.Domain.Models.Sorting;
using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetAllProductsWithSortingQuery : IRequest<IEnumerable<Product>>
    {
        public SortingRequest Sorting { get; set; }

        public GetAllProductsWithSortingQuery(string? sortingField, string? sortDirection)
        {
            Sorting = new SortingRequest
            {
                Field = sortingField != null ? sortingField : "Id",
                Direction = sortDirection != null
                    ? sortDirection.ToLower() == "asc"
                        ? SortDirection.Asc
                        : SortDirection.Desc
                    : SortDirection.Asc
            };
        }
    }
}
