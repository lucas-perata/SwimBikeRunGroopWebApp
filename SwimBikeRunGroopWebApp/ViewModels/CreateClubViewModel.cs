﻿using SwimBikeRunGroopWebApp.Data.Enum;
using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.ViewModels
{
    public class CreateClubViewModel
    {
        public int ClubId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Strava { get; set; }
        public bool IsWomensOnly { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public Address Address { get; set; }
    }
}
