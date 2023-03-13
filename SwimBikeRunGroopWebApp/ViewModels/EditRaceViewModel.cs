using SwimBikeRunGroopWebApp.Data.Enum;

namespace SwimBikeRunGroopWebApp.ViewModels
{
    public class EditRaceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public SportCategory SportCategory { get; set; }
    }
}
