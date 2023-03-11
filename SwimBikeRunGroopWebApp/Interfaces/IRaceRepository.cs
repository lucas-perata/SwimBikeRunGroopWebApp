using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<IEnumerable<Race>> GetRaceByLocation(string location);
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(Race race);
        bool Save(); 
    }
}
