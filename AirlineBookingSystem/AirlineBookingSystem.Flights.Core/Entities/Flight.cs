using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlineBookingSystem.Flights.Core.Entities
{
    //public class Flight
    //{
    //    public Guid Id { get; set; }
    //    public string? FlightNumber { get; set; }
    //    public string? Origin { get; set; }
    //    public string? Destination { get; set; }
    //    public DateTime ArrivalTime { get; set; }
    //    public DateTime DepartureTime { get; set; }
    //}
    public class Flight
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonElement("FlightNumber")]
        public string? FlightNumber { get; set; }
        [BsonElement("Origin")]
        public string? Origin { get; set; }
        [BsonElement("Destination")]
        public string? Destination { get; set; }
        [BsonElement("DepartureTime")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DepartureTime { get; set; }
        [BsonElement("ArrivalTime")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ArrivalTime { get; set; }
    }
}
