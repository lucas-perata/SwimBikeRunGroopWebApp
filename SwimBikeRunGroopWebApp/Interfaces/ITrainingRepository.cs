using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Interfaces
{
    public interface ITrainingRepository
    {
        Task<IEnumerable<Training>> GetAll();
        Task<Training> GetById(int id);
        Task<Training> GetByIdNoTracking(int id);
        Task<IEnumerable<Training>> GetTrainingByAddress(string startAddress);
        bool Add(Training training);
        bool Update(Training training);
        bool Delete(Training training);
        bool Save();
    }
}
