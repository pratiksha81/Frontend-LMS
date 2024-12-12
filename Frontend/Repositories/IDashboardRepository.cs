using Frontend.Models;

namespace Frontend.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashBoard> GetDashboardDataAsync();
        Task<List<OverdueBorrower>> GetOverdueBorrowersAsync();
    }
}
