using FlightNotificationSystem.Data;
using FlightNotificationSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightNotificationSystem.AlertManagement.API.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly ApplicationDbContext _context;

        public AlertRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alert>> GetAlertsAsync()
        {
            return await _context.Alerts.Include(a => a.User).ToListAsync();
        }

        public async Task<Alert> GetAlertByIdAsync(int id)
        {
            return await _context.Alerts.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAlertAsync(Alert alert)
        {
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAlertAsync(Alert alert)
        {
            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlertAsync(int id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            if (alert != null)
            {
                _context.Alerts.Remove(alert);
                await _context.SaveChangesAsync();
            }
        }
    }
}
