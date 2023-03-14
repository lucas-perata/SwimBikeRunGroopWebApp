using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Race>? Races { get; set; }
        public IEnumerable<Club>? Clubs { get; set; }
        public IEnumerable<Training>? Trainings { get; set; }
    }
}
