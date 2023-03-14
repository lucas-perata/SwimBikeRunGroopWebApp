using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.ViewModels
{
    public class DashboardViewModel
    {
        public List<Race> Races { get; set; }
        public List<Club> Clubs { get; set; }
        public List<Training> Trainings { get; set; }
    }
}
