using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Models;
using System;
using System.Dynamic;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClubController(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public IActionResult Index()
        {
            var clubs = _context.Clubs.ToList();
            return View(clubs);
        }

        public IActionResult Detail(int id)
        {
            var club = _context.Clubs.Where(c => c.Id == id).Include(c => c.Address).Include(c => c.Training).FirstOrDefault();
            return View(club);
        }
    }
}
