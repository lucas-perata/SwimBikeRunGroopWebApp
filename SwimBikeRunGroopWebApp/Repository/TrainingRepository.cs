using Microsoft.EntityFrameworkCore;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Repository
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Training training)
        {
            _context.Add(training);
            return Save();
        }

        public async Task<Club> AdminClub(string id)
        {
            var adminClub = await _context.Clubs.Where(c => c.AppUserId == id).FirstOrDefaultAsync();
            return adminClub;
        }

        public bool Delete(Training training)
        {
            _context.Remove(training);
            return Save();
        }

        public async Task<IEnumerable<Training>> GetAll()
        {
            return await _context.Trainings.ToListAsync();
        }

        public async Task<Training> GetById(int id)
        {
            return await _context.Trainings.Where(t => t.Id == id).Include(c => c.Club).FirstOrDefaultAsync();
        }
        public async Task<Training> GetByIdNoTracking(int id)
        {
            return await _context.Trainings.Where(t => t.Id == id).Include(c => c.Club).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Training>> GetTrainingByAddress(string startAddress)
        {
            return await _context.Trainings.Where(t => t.StartAddress == startAddress).ToListAsync();
        }

        public bool Save()
        {
            var save =_context.SaveChanges();
            return save > 0 ? true : false; 
        }

        public bool Update(Training training)
        {
            _context.Update(training);
            return Save();
        }
    }
}
