using AirlineBookingSystem.Flights.Core.Entities;
using AirlineBookingSystem.Flights.Core.Repositories;
using AirlineBookingSystem.Flights.Infrastructure.Data;
using Dapper;
using MongoDB.Driver;
using System.Data;

namespace AirlineBookingSystem.Flights.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        #region Code before Mongo Implementation
        //private readonly IDbConnection _dbConnection;

        //public FlightRepository(IDbConnection dbConnection)
        //{
        //    _dbConnection = dbConnection;
        //}

        //public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        //{
        //    const string sql = @"
        //    SELECT
        //        Id,
        //        FlightNumber,
        //        Origin,
        //        Destination,
        //        ArrivalTime,
        //        DepartureTime
        //    FROM Flights";

        //    return await _dbConnection.QueryAsync<Flight>(sql);
        //}

        //public async Task AddFlightAsync(Flight flight)
        //{
        //    const string sql = @"
        //    INSERT INTO Flights
        //    (
        //        Id,
        //        FlightNumber,
        //        Origin,
        //        Destination,
        //        ArrivalTime,
        //        DepartureTime
        //    )
        //    VALUES
        //    (
        //        @Id,
        //        @FlightNumber,
        //        @Origin,
        //        @Destination,
        //        @ArrivalTime,
        //        @DepartureTime
        //    );";

        //    await _dbConnection.ExecuteAsync(sql, flight);
        //}

        //public async Task DeleteFlightAsync(Guid flightId)
        //{
        //    const string sql = @"
        //    DELETE FROM Flights
        //    WHERE Id = @Id;";

        //    await _dbConnection.ExecuteAsync(sql, new { Id = flightId });
        //}

        //public async Task<Flight> GetFlightByIdAsync(Guid flightId)
        //{
        //    const string sql = @"
        //    SELECT
        //        Id,
        //        FlightNumber,
        //        Origin,
        //        Destination,
        //        ArrivalTime,
        //        DepartureTime
        //    FROM Flights
        //    WHERE Id = @Id;";

        //    return await _dbConnection.QuerySingleAsync<Flight>(sql, new { Id = flightId });
        //}
        #endregion

        private readonly IFlightContext _context;

        public FlightRepository(IFlightContext context)
        {
            _context = context;
        }

        public async Task AddFlightAsync(Flight flight)
        {
            await _context.Flights.InsertOneAsync(flight);
        }

        public async Task DeleteFlightAsync(Guid id)
        {
            await _context.Flights.DeleteOneAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Flight>> GetFlightsAsync()
        {
            return await _context.Flights.Find(flight => true).ToListAsync();
        }

    }
}
