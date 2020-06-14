using RPS.Core.Models.Dto;

namespace RPS.Data
{
    public interface IPtDashboardRepository
    {
        PtDashboardStatusCounts GetStatusCounts(PtDashboardFilter filter);
        PtDashboardFilteredIssues GetFilteredIssues(PtDashboardFilter filter);
    }
}
