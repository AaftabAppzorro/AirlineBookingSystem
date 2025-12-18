using AirlineBookingSystem.Bookings.Core.Entities;
using MediatR;

namespace AirlineBookingSystem.Bookings.Application.Queries
{
    public record GetBookingQueryById(Guid Id) : IRequest<Booking>;
}
