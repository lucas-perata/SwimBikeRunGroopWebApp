namespace SwimBikeRunGroopWebApp.ViewModels
{
    public class EditUserDashboardViewModel
    {
        public string Id { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Town { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public int? Pace { get; set; }
        public int? RunWeekly { get; set; }
        public int? Swim { get; set; }
        public int? SwimWeekly { get; set; }
        public int? Bike { get; set; }
        public int? BikeWeekly { get; set; }
        public IFormFile Image { get; set; }
    }
}
