using SwimBikeRunGroopWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimBikeRunGroopWebApp.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int DistanceFromStart { get; set; }
        public string StartAddress { get; set; }
        public int AveragePace { get; set; }
        public SportCategory SportCategory { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId {get; set; }
        public AppUser? AppUser { get; set; }
    }
}
