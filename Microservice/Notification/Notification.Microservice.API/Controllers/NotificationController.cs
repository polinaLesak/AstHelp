using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Microservice.Application.Queries;
using Orders.Microservice.Application.Commands;
using NotificationEntity = Notification.Microservice.Domain.Entities.Notification;

namespace Notification.Microservice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Notification?userId=
        [HttpGet]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<IEnumerable<NotificationEntity>> GetAllNotificationsByUserId(
            [FromQuery] int userId)
        {
            return await _mediator.Send(new GetAllNotificationsByUserIdQuery(userId));
        }

        // POST: api/Notification
        [HttpPost]
        [Authorize(Roles = "1, 2, 3")]
        public async Task<NotificationEntity> AddNotificationToUserId(
            [FromBody] AddNotificationCommand command)
        {
            return await _mediator.Send(command);
        }

        // PUT: api/Notification/MarkAsRead?id=
        [HttpPut("MarkAsRead")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task MarkNotificationAsReadById(
            [FromQuery] Guid id)
        {
            await _mediator.Send(new MarkNotificationAsReadByIdCommand(id));
        }

        // POST: api/Notification/All/MarkAsRead?userId=
        [HttpPost("All/MarkAsRead")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task MarkAllNotificationsAsReadByUserId([FromQuery] int userId)
        {
            await _mediator.Send(new MarkAllNotificationsAsReadByUserIdCommand(userId));
        }

        // DELETE: api/Notification?id=
        [HttpDelete]
        [Authorize(Roles = "1, 2, 3")]
        public async Task DeleteNotificationById(
            [FromQuery] Guid id)
        {
            await _mediator.Send(new DeleteNotificationByIdCommand(id));
        }

        // DELETE: api/Notification/All?userId=
        [HttpDelete("All")]
        [Authorize(Roles = "1, 2, 3")]
        public async Task DeleteAllNotificationsByUserId([FromQuery] int userId)
        {
            await _mediator.Send(new DeleteAllNotificationsByUserIdCommand(userId));
        }
    }
}
