using MediatR;
using Microsoft.AspNetCore.Http;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Excel;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;
using System.Security.Claims;

namespace Orders.Microservice.Application.Handlers
{
    public class GenerateAllOrdersReportCommandHandler : IRequestHandler<GenerateAllOrdersReportCommand, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenerateAllOrdersReportCommandHandler(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<byte[]> Handle(GenerateAllOrdersReportCommand request, CancellationToken cancellationToken)
        {
            var orders = new List<Order>();
            var user = _httpContextAccessor.HttpContext?.User;
            if (user != null && user.Identity?.IsAuthenticated == true)
            {
                if (user.Claims.Any(c =>
                    c.Type == ClaimTypes.Role && c.Value == "1"))
                {
                    orders = (await _unitOfWork.Orders.GetAllAsync()).ToList();
                }
                else if (user.Claims.Any(c =>
                    c.Type == ClaimTypes.Role && c.Value == "2"))
                {
                    orders = (await _unitOfWork.Orders.GetOrdersByManagerIdAsync(int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value))).ToList();
                }
            }
            else
            {
                return [];
            }

            return await AllOrdersReportExcel.ProcessExcel(orders);
        }
    }
}
