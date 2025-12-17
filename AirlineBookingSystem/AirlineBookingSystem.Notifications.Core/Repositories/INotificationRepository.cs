using AirlineBookingSystem.Notifications.Core.Entities;

namespace AirlineBookingSystem.Notifications.Core.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> LogNotificationAsync(Notification notification);
    }
}
