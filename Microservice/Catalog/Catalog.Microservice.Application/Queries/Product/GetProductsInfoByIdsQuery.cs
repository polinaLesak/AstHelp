using Catalog.Microservice.Domain.Entities;
using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetProductsInfoByIdsQuery : IRequest<List<Product>>
    {
        public Guid[] Ids { get; set; }

        public GetProductsInfoByIdsQuery(Guid[] ids)
        {
            Ids = ids;
        }
    }
}
