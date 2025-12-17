using AirlineBookingSystem.Flights.Core.Entities;

namespace AirlineBookingSystem.Flights.Core.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetFlightByIdAsync(Guid flightId);
        Task AddFlightAsync(Flight flight);
        Task DeleteFlightAsync(Guid flightId);
    }
}
