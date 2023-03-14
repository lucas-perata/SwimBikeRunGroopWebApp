using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
        Task<List<Training>> GetAllUserTrainings(); 
    }
}
