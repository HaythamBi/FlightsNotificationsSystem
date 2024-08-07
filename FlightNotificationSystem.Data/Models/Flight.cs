namespace FlightNotificationSystem.Data.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public decimal Price { get; set; }
    }
}
