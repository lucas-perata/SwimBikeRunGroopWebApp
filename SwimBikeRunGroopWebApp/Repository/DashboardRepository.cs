using Microsoft.EntityFrameworkCore;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;    
        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userClubs = await _context.Clubs.Where(r => r.AppUser.Id == curUser.ToString()).ToListAsync();
            return userClubs;
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userRaces = await  _context.Races.Where(r => r.AppUser.Id == curUser.ToString()).ToListAsync();
            return userRaces;
        }

        public async Task<List<Training>> GetAllUserTrainings()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userTrainings = await _context.Trainings.Where(r => r.AppUser.Id == curUser.ToString()).ToListAsync(); 
            return userTrainings;
        }
    }
}
