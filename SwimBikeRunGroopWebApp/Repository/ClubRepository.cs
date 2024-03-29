﻿using Microsoft.EntityFrameworkCore;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club);
            return Save();
        }

        public bool AddUserToClub(UserClub userClub)
        {
            _context.Add(userClub);
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public bool DeleteUserOfClub(UserClub userClub)
        {   

            _context.Remove(userClub);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(c => c.Address).Include(c => c.Training).FirstOrDefaultAsync(i => i.ClubId == id);
        }
        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(c => c.Address).Include(c => c.Training).AsNoTracking().FirstOrDefaultAsync(i => i.ClubId == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<UserClub> GetUserClubById(int clubId, string appUserId)
        {
            var userClub = await _context.Clubs.Include(c => c.UserClubs).SingleOrDefaultAsync(c => c.ClubId == clubId);
            var userClubId = userClub.UserClubs.Where(uc => uc.AppUserId == appUserId).SingleOrDefault();
            return userClubId;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }

        public bool UserHasClub(string id)
        {
            var userClub = _context.Clubs.Where(c => c.AppUserId == id).FirstOrDefault();
            return userClub != null;
        }
        public bool UserIsInClub(int clubId, string appUserId)
        {
            var userIsInClub = _context.Clubs.Include(c => c.UserClubs).SingleOrDefault(c => c.ClubId == clubId);
            var hasUser = userIsInClub.UserClubs.Any(uc => uc.AppUserId == appUserId);
            return hasUser;
        }
    }
}
