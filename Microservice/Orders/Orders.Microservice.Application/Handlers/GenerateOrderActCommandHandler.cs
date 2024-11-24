using MediatR;
using Microsoft.AspNetCore.Http;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.DTOs;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Application.Service;
using Orders.Microservice.Application.Word;
using Orders.Microservice.Domain.Repositories;
using System.Security.Claims;

namespace Orders.Microservice.Application.Handlers
{
    public class GenerateOrderActCommandHandler : IRequestHandler<GenerateOrderActCommand, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IdentityService _identityService;

        public GenerateOrderActCommandHandler(
            IUnitOfWork unitOfWork,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor,
            IdentityService identityService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
        }

        public async Task<byte[]> Handle(GenerateOrderActCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"Заказ с ID {request.OrderId} не найден.");

            var user = _httpContextAccessor.HttpContext?.User;
            var adminInfo = await _identityService.GetUserInfoById(
                int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (adminInfo == null)
                throw new NotFoundException("Информация о пользователе не найдена.");

            using var templateStream = _fileService.GetTemplateStream("ActTemplate.docx");

            return await OrderActWord.ProcessWord(templateStream, new OrderActDto
            {
                Order = order,
                CustomerFullname = order.CustomerFullname,
                CustomerPosition = order.CustomerPosition,
                AdminFullname = adminInfo.Profile.Fullname,
                AdminPosition = adminInfo.Profile.Position
            });
        }
    }
}
