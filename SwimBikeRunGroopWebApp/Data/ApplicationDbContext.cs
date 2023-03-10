using Microsoft.EntityFrameworkCore;
using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Race> Races { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Training> Trainings { get; set; }
    }
}
