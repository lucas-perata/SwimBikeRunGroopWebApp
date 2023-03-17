using System.ComponentModel.DataAnnotations;

namespace SwimBikeRunGroopWebApp.Models
{
    public class UserClub
    {
        [Key]
        public int Id { get; set; } 
        public int ClubId { get; set; }
        public Club Club { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
