using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
        Task<List<Training>> GetAllUserTrainings();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        public bool Update(AppUser user);
        public bool Save();
    }
}
