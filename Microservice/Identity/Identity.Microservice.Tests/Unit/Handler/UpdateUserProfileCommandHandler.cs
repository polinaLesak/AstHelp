using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.Exceptions;
using Identity.Microservice.Application.Handlers;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using Moq;

namespace Identity.Microservice.Tests.Unit.Handler
{
    public class UpdateUserProfileCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly UpdateUserProfileCommandHandler _handler;

        public UpdateUserProfileCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateUserProfileCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ProfileNotFound_ThrowsNotFoundException()
        {
            var command = new UpdateUserProfileCommand { UserId = 1 };
            _unitOfWorkMock.Setup(u => u.Profiles.GetByUserIdAsync(command.UserId))
                .ReturnsAsync((Profile)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ProfileFound_UpdatesProfileAndCommits()
        {
            var existingProfile = new Profile { Fullname = "Old Name", Position = "Old Position" };
            var command = new UpdateUserProfileCommand
            {
                UserId = 1,
                Fullname = "New Name",
                Position = "New Position"
            };

            _unitOfWorkMock.Setup(u => u.Profiles.GetByUserIdAsync(command.UserId))
                .ReturnsAsync(existingProfile);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal("New Name", existingProfile.Fullname);
            Assert.Equal("New Position", existingProfile.Position);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
