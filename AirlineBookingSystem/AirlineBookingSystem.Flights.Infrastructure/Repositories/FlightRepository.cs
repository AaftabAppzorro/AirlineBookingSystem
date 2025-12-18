using AirlineBookingSystem.Flights.Core.Entities;
using AirlineBookingSystem.Flights.Core.Repositories;
using Dapper;
using System.Data;

namespace AirlineBookingSystem.Flights.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IDbConnection _dbConnection;

        public FlightRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            const string sql = @"
            SELECT
                Id,
                FlightNumber,
                Origin,
                Destination,
                ArrivalTime,
                DepartureTime
            FROM Flights";

            return await _dbConnection.QueryAsync<Flight>(sql);
        }

        public async Task AddFlightAsync(Flight flight)
        {
            const string sql = @"
            INSERT INTO Flights
            (
                Id,
                FlightNumber,
                Origin,
                Destination,
                ArrivalTime,
                DepartureTime
            )
            VALUES
            (
                @Id,
                @FlightNumber,
                @Origin,
                @Destination,
                @ArrivalTime,
                @DepartureTime
            );";

            await _dbConnection.ExecuteAsync(sql, flight);
        }

        public async Task DeleteFlightAsync(Guid flightId)
        {
            const string sql = @"
            DELETE FROM Flights
            WHERE Id = @Id;";

            await _dbConnection.ExecuteAsync(sql, new { Id = flightId });
        }

        public async Task<Flight> GetFlightByIdAsync(Guid flightId)
        {
            const string sql = @"
            SELECT
                Id,
                FlightNumber,
                Origin,
                Destination,
                ArrivalTime,
                DepartureTime
            FROM Flights
            WHERE Id = @Id;";

            return await _dbConnection.QuerySingleAsync<Flight>(sql, new { Id = flightId });
        }
    }
}
