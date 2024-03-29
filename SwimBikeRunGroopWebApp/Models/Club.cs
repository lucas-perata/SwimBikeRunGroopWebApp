﻿using SwimBikeRunGroopWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimBikeRunGroopWebApp.Models
{
    public class Club
    {
        [Key]
        public int ClubId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Strava { get; set; }
        public bool IsWomensOnly { get; set; }
        public ClubCategory ClubCategory { get; set; }

        public ICollection<Training> Training { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<UserClub> UserClubs { get; set; }

    }
}
