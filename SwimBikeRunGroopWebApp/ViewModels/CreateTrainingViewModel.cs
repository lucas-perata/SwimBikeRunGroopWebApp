using SwimBikeRunGroopWebApp.Data.Enum;
using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.ViewModels
{
    public class CreateTrainingViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DistanceFromStart { get; set; }
        public string StartAddress { get; set; }
        public int AveragePace { get; set; }
        public string AppUserId { get; set; }
        public SportCategory SportCategory { get; set; }

        public Club? Club { get; set; }
        public int? ClubId { get; set; }
   
    }
}
