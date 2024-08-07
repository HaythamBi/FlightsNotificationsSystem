namespace FlightNotificationSystem.Data.Models
{
    public class UserAlert
    {
        public int UserAlertId { get; set; }
        public string UserId { get; set; }
        public int AlertId { get; set; }

        public Alert Alert { get; set; }
    }

}
