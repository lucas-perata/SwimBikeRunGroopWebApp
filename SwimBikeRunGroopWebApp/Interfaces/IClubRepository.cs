using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int id);
        Task<Club> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Club>> GetClubByCity(string city);

        bool AddUserToClub(UserClub userClub);
        bool UserIsInClub(int clubId, string appUserId);
        bool UserHasClub(string id);
        bool Add(Club club);
        bool Update(Club club);
        bool Delete(Club club);
        bool Save();
    }
}
