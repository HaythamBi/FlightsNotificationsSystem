using System.Text.Json;

namespace FlightNotificationSystem.Shared.Models
{
    public class AlertMessage
    {
        public int FlightId { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static AlertMessage FromJson(string json)
        {
            return JsonSerializer.Deserialize<AlertMessage>(json);
        }
    }
}
