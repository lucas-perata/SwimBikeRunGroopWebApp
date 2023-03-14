using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimBikeRunGroopWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public int Pace { get; set; }
        public int RunWeekly { get; set; }
        public int Swim { get; set; }
        public int SwimWeekly { get; set; }
        public int Bike { get; set; }
        public int BikeWeekly { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Training> Trainings { get; set; }
        public ICollection<Race> Races { get; set; }
    }
}
