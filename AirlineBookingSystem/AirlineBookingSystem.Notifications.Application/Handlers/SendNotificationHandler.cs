using AirlineBookingSystem.Notifications.Application.Commands;
using AirlineBookingSystem.Notifications.Core.Entities;
using AirlineBookingSystem.Notifications.Core.Repositories;
using MediatR;

namespace AirlineBookingSystem.Notifications.Application.Handlers
{
    public class SendNotificationHandler : IRequestHandler<SendNotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository;

        public SendNotificationHandler(INotificationRepository notificationService)
        {
            _notificationRepository = notificationService;
        }

        public async Task Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Recipient = request.Recipient,
                Message = request.Message,
                Type = request.Type,
                SentAt = DateTime.UtcNow
            };

            await _notificationRepository.LogNotificationAsync(notification);
        }
    }
}
