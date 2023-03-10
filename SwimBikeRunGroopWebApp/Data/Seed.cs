using Microsoft.AspNetCore.Identity;
using SwimBikeRunGroopWebApp.Data.Enum;
using SwimBikeRunGroopWebApp.Models;
using SwimBikeRunGroopWebApp.Data;

namespace RunGroopWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Running Club 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.Running,
                            Strava = "strava.com",
                            IsWomensOnly = false,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                Province = "BsAs"
                            }
                         },
                        new Club()
                        {
                            Title = "Swimming Club 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.Swimming,
                             Strava = "strava.com",
                            IsWomensOnly = false,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                Province = "BsAs"
                            }
                        },
                        new Club()
                        {
                            Title = "Duathlon Club 3",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.Duathlon,
                             Strava = "strava.com",
                            IsWomensOnly = false,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                Province = "BsAs"
                            }
                        },
                        new Club()
                        {
                            Title = "Triathlon Club 3",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.Triathlon,
                             Strava = "strava.com",
                            IsWomensOnly = false,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Michigan",
                                Province = "BsAs"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Races.Any())
                {
                    context.Races.AddRange(new List<Race>()
                    {
                        new Race()
                        {
                            Title = "Cycling Race 1",
                            Description = "This is the description of the first race",
                            SportCategory = SportCategory.Cycling,
                            Location = "Morón"

                        },
                        new Race()
                        {
                            Title = "Running Race 2",
                            Description = "This is the description of the first race",
                            SportCategory = SportCategory.Running,
                            Location = "Morón"
                        }
                    }); ;
                    context.SaveChanges();
                }
                //Training
                if (!context.Trainings.Any())
                {
                    context.Trainings.AddRange(new List<Training>()
                    {
                        new Training()
                        {   
                            Title = "Training 1",
                            DistanceFromStart = 60,
                            StartAddress = "Posta",
                            SportCategory = SportCategory.Cycling,
                            AveragePace = 25
                        },
                        new Training()
                        {
                            Title = "Training 2",
                            DistanceFromStart = 5,
                            StartAddress = "Posta",
                            SportCategory = SportCategory.Running,
                            AveragePace = 8
                        }
                    }); ;
                    context.SaveChanges();
                }
            }
        }

        /* public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            Province = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            Province = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        } */
    }
}