using AirlineBookingSystem.Flights.Core.Entities;

namespace AirlineBookingSystem.Flights.Core.Repositories
{
    public interface IFlightRepository
    {
        //Task<IEnumerable<Flight>> GetAllFlightsAsync();
        //Task<Flight> GetFlightByIdAsync(Guid flightId);
        //Task AddFlightAsync(Flight flight);
        //Task DeleteFlightAsync(Guid flightId);

        Task<IEnumerable<Flight>> GetFlightsAsync();
        Task AddFlightAsync(Flight flight);
        Task DeleteFlightAsync(Guid id);
    }
}
