using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System;

namespace TasksApi.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
          
            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = "Developer",
                    NormalizedName = "DEVELOPER"
                }
            );

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "b74ddd14-6340-4840-95c2-db12554843e5",      // primary key
                    UserName = "admin@gtc.edu.ps",
                    Email = "admin@gtc.edu.ps",
                    NormalizedUserName = "ADMIN@GTC.EDU.PS",
                    NormalizedEmail= "ADMIN@GTC.EDU.PS",
                    PasswordHash = hasher.HashPassword(null, "Gtc_2023"),
                    EmailConfirmed=true,
                },
                 new IdentityUser
                 {
                     Id = "b74ddd14-6340-4840-95c2-db12554843cc",      // primary key
                     UserName = "super@gtc.edu.ps",
                     Email = "super@gtc.edu.ps",
                     NormalizedUserName = "SUPER@GTC.EDU.PS",
                     NormalizedEmail = "SUPER@GTC.EDU.PS",
                     PasswordHash = hasher.HashPassword(null, "Gtc_2023"),
                     EmailConfirmed = true,
                 },
                new IdentityUser
                {
                    Id = "b74ddd14-6340-4840-95c2-db12554843bb",      // primary key
                    UserName = "user2@gtc.edu.ps",
                    Email = "user2@gtc.edu.ps",
                    NormalizedUserName = "USER2@GTC.EDU.PS",
                    NormalizedEmail = "USER2@GTC.EDU.PS",

                    PasswordHash = hasher.HashPassword(null, "Gtc_2023"),
                    EmailConfirmed = true,
                },
                new IdentityUser
                {
                    Id = "b74ddd14-6340-4840-95c2-db12554843aa",      // primary key
                    UserName = "user1@gtc.edu.ps",
                    Email = "user1@gtc.edu.ps",
                    NormalizedUserName = "USER1@GTC.EDU.PS",
                    NormalizedEmail = "USER2@GTC.EDU.PS",
                    PasswordHash = hasher.HashPassword(null, "Gtc_2023"),
                    EmailConfirmed = true,
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>()
             .HasData(
                   new IdentityUserRole<string>
                   {
                       UserId = "b74ddd14-6340-4840-95c2-db12554843e5",
                       RoleId = "1"
                   },
                   new IdentityUserRole<string>
                   {
                       UserId = "b74ddd14-6340-4840-95c2-db12554843cc",
                       RoleId = "2"
                   },
                   new IdentityUserRole<string>
                   {
                       UserId = "b74ddd14-6340-4840-95c2-db12554843bb",
                       RoleId = "3"
                   },
                   new IdentityUserRole<string>
                   {
                       UserId = "b74ddd14-6340-4840-95c2-db12554843aa",
                       RoleId = "3"
                   }
                   );



            //modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
            //    new IdentityUserClaim<string>
            //    {
            //        Id= 1,
            //        UserId= "b74ddd14-6340-4840-95c2-db12554843e5",
            //        ClaimType ="Permission",
            //        ClaimValue="Admin"
            //    },
            //     new IdentityUserClaim<string>
            //     {
            //         Id = 2,
            //         UserId = "b74ddd14-6340-4840-95c2-db12554843cc",
            //         ClaimType = "Permission",
            //         ClaimValue = "Supervisor"
            //     },
            //    new IdentityUserClaim<string>
            //    {
            //        Id = 3,
            //        UserId = "b74ddd14-6340-4840-95c2-db12554843aa",
            //        ClaimType = "Permission",
            //        ClaimValue = "User"
            //    },
            //    new IdentityUserClaim<string>
            //    {
            //        Id = 4,
            //        UserId = "b74ddd14-6340-4840-95c2-db12554843bb",
            //        ClaimType = "Permission",
            //        ClaimValue = "User"
            //    },
            //    new IdentityUserClaim<string>
            //    {
            //        Id = 5,
            //        UserId = "b74ddd14-6340-4840-95c2-db12554843e5",
            //        ClaimType = "Sign",
            //        ClaimValue = "2"
            //    },
            //     new IdentityUserClaim<string>
            //     {
            //         Id = 6,
            //         UserId = "b74ddd14-6340-4840-95c2-db12554843cc",
            //         ClaimType = "Sign",
            //         ClaimValue = "1"
            //     }
            // );

        }


        //public static void Initialize(IServiceProvider serviceProvider)
        //{
        //    var context = serviceProvider.GetService<ApplicationDbContext>();

        //    string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

        //    foreach (string role in roles)
        //    {
        //        var roleStore = new RoleStore<IdentityRole>(context);

        //        if (!context.Roles.Any(r => r.Name == role))
        //        {
        //            roleStore.CreateAsync(new IdentityRole(role));
        //        }
        //    }

        //    var user = new ApplicationUser
        //    {
        //        FirstName = "XXXX",
        //        LastName = "XXXX",
        //        Email = "xxxx@example.com",
        //        NormalizedEmail = "XXXX@EXAMPLE.COM",
        //        UserName = "Owner",
        //        NormalizedUserName = "OWNER",
        //        PhoneNumber = "+111111111111",
        //        EmailConfirmed = true,
        //        PhoneNumberConfirmed = true,
        //        SecurityStamp = Guid.NewGuid().ToString("D")
        //    };

        //    if (!context.Users.Any(u => u.UserName == user.UserName))
        //    {
        //        var password = new PasswordHasher<ApplicationUser>();
        //        var hashed = password.HashPassword(user, "secret");
        //        user.PasswordHash = hashed;

        //        var userStore = new UserStore<ApplicationUser>(context);
        //        var result = userStore.CreateAsync(user);
        //    }

        //    AssignRoles(serviceProvider, user.Email, roles);

        //    context.SaveChangesAsync();
        //}

        public static  void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            //Dictionary<string, string> clims = new Dictionary<string, string>();
            //clims.Add("Permission", "Admin");
            //clims.Add("Sign", "2");
            //_ =AssignClaims(serviceProvider,"admin@gtc.edu.ps", clims);

            //clims.Clear();
            //clims.Add("Permission", "Supervisor");
            //clims.Add("Sign", "1");
            //_ = AssignClaims(serviceProvider, "super@gtc.edu.ps", clims);

            //clims.Clear();
            //clims.Add("Permission", "User");
            //_=AssignClaims(serviceProvider, "user1@gtc.edu.ps", clims);

            //clims.Clear();
            //clims.Add("Permission", "User");
            //_ = AssignClaims(serviceProvider, "user2@gtc.edu.ps", clims);



            context.SaveChanges();
        }

        public static async Task<IdentityUser> AssignClaims(IServiceProvider services, string email, Dictionary<string,string> claims)
        {
            UserManager<IdentityUser>? _userManager = services.GetService<UserManager<IdentityUser>>();
            IdentityUser? user = await _userManager.FindByEmailAsync(email);
            var existingUserClaims = await _userManager.GetClaimsAsync(user);

            foreach (var claim in claims)
            {
                Claim nc = new Claim(claim.Key, claim.Value);
                if (!existingUserClaims.Contains(nc))
                {
                    IdentityResult result = await _userManager.AddClaimAsync(user, nc);
                }
            }

            return user;
        }


        public static void Seed1(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // use context
        }

    }
}
