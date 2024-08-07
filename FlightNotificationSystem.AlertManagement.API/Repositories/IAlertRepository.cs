using FlightNotificationSystem.Data.Models;

namespace FlightNotificationSystem.AlertManagement.API.Repositories
{
    public interface IAlertRepository
    {
        Task<IEnumerable<Alert>> GetAlertsAsync();
        Task<Alert> GetAlertByIdAsync(int id);
        Task AddAlertAsync(Alert alert);
        Task UpdateAlertAsync(Alert alert);
        Task DeleteAlertAsync(int id);
    }
}
