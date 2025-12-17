using AirlineBookingSystem.Bookings.Core.Entities;

namespace AirlineBookingSystem.Bookings.Core.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByIdAsync(Guid bookingId);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Guid bookingId);
    }
}
