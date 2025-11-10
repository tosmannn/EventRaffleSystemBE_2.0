using EventRaffle.Core.DTOs.Dashboard;
using EventRaffle.Core.Models;

namespace EventRaffle.Core.Interfaces.Services
{
    public interface IDashbaordService
    {
        Task<ResultModel<DashboardDto>> GetDashboardSummary(); 
    }
}
