using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? Pace { get; set; }
        public int? RunWeekly { get; set; }
        public int? Swim { get; set; }
        public int? SwimWeekly { get; set; }
        public int? Bike { get; set; }
        public int? BikeWeekly { get; set; }

        public ICollection<Club>? Clubs { get; set; }
        public ICollection<Training>? Trainings { get; set; }
        public ICollection<Race>? Races { get; set; }
    }
}
