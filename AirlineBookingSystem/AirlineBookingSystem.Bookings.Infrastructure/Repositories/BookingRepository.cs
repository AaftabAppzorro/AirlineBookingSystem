using AirlineBookingSystem.Bookings.Core.Entities;
using AirlineBookingSystem.Bookings.Core.Repositories;
using Dapper;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Data;

namespace AirlineBookingSystem.Bookings.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        #region Using Dapper
        //private readonly IDbConnection _dbConnection;

        //public BookingRepository(IDbConnection dbConnection)
        //{
        //    _dbConnection = dbConnection;
        //}

        //public async Task AddBookingAsync(Booking booking)
        //{
        //    const string sql = @"
        //    INSERT INTO Bookings
        //    (
        //        Id,
        //        FlightId,
        //        PassengerName,
        //        SeatNumber,
        //        BookingDate
        //    )
        //    VALUES
        //    (
        //        @Id,
        //        @FlightId,
        //        @PassengerName,
        //        @SeatNumber,
        //        @BookingDate
        //    );";

        //    await _dbConnection.ExecuteAsync(sql, booking);
        //}

        //public async Task DeleteBookingAsync(Guid bookingId)
        //{
        //    const string sql = @"
        //    DELETE FROM Bookings
        //    WHERE Id = @Id;";

        //    await _dbConnection.ExecuteAsync(
        //        sql,
        //        new { Id = bookingId }
        //    );
        //}

        //public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        //{
        //    const string sql = @"
        //    SELECT 
        //        Id,
        //        FlightId,
        //        PassengerName,
        //        SeatNumber,
        //        BookingDate
        //    FROM Bookings
        //    WHERE Id = @Id;";

        //    return await _dbConnection.QueryFirstOrDefaultAsync<Booking>(sql, new { Id = bookingId });
        //}

        //public async Task UpdateBookingAsync(Booking booking)
        //{
        //    const string sql = @"
        //    UPDATE Bookings
        //    SET
        //        FlightId = @FlightId,
        //        PassengerName = @PassengerName,
        //        SeatNumber = @SeatNumber,
        //        BookingDate = @BookingDate
        //    WHERE Id = @Id;";

        //    await _dbConnection.ExecuteAsync(sql, booking);
        //}
        #endregion

        #region Using Redis
        private readonly IDatabase _redisDb;
        private const string BookingKeyPrefixForRedis = "booking_";

        public BookingRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _redisDb = connectionMultiplexer.GetDatabase();
        }

        public async Task AddBookingAsync(Booking booking)
        {
            var data = JsonConvert.SerializeObject(booking);
            await _redisDb.StringSetAsync(BookingKeyPrefixForRedis, data);
        }

        public async Task DeleteBookingAsync(Guid bookingId)
        {
            var key = $"{BookingKeyPrefixForRedis}{bookingId}";
            await _redisDb.KeyDeleteAsync(key);
        }

        public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        {
            var key = $"{BookingKeyPrefixForRedis}{bookingId}";
            var data = await _redisDb.StringGetAsync(key)!;
            return string.IsNullOrEmpty(data) ? null : JsonConvert.DeserializeObject<Booking>(data);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            var key = $"{BookingKeyPrefixForRedis}{booking.Id}";
            var data = JsonConvert.SerializeObject(booking);
            await _redisDb.StringSetAsync(key, data);
        }
        #endregion
    }
}
