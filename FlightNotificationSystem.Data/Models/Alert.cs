namespace FlightNotificationSystem.Data.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FlightNumber { get; set; }
        public decimal TargetPrice { get; set; }
        public DateTime AlertDate { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
