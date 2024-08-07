using FlightNotificationSystem.AlertManagement.API.Repositories;
using FlightNotificationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightNotificationSystem.AlertManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertRepository _alertRepository;

        public AlertController(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlerts()
        {
            var alerts = await _alertRepository.GetAlertsAsync();
            return Ok(alerts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlert(int id)
        {
            var alert = await _alertRepository.GetAlertByIdAsync(id);
            if (alert == null)
            {
                return NotFound();
            }

            return Ok(alert);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlert([FromBody] Alert alert)
        {
            await _alertRepository.AddAlertAsync(alert);
            return CreatedAtAction(nameof(GetAlert), new { id = alert.Id }, alert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlert(int id, [FromBody] Alert alert)
        {
            if (id != alert.Id)
            {
                return BadRequest();
            }

            await _alertRepository.UpdateAlertAsync(alert);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id)
        {
            await _alertRepository.DeleteAlertAsync(id);
            return NoContent();
        }
    }
}
