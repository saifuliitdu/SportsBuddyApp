using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Edm.Csdl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsBuddy.Models;
using SportsBuddy.StaticData;
using SportsBuddyApp.Interface;
using SportsBuddyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddy.Data
{
    public static class SeedData<TIdentityUser, TIdentityRole>
        where TIdentityUser : ApplicationUser, new()
        where TIdentityRole : IdentityRole, new()
    {
        private const string DefaultAdminRoleName = Role.SuperAdmin;
        private const string DefaultAdminUserEmail = "saiful@gmail.com";
        private const string DefaultAdminUserPassword = "123456aA@";

        private static readonly string[] DefaultRoles = { Role.SuperAdmin, Role.Admin, Role.ClientAdmin, Role.ClientUser, Role.KeyPerson, Role.QC, Role.User, Role.HR };

        private static async Task CreateDefaultRoles(RoleManager<TIdentityRole> roleManager)
        {
            foreach (string defaultRole in DefaultRoles)
            {
                // Make sure we have an Administrator role
                try
                {
                    if (!await roleManager.RoleExistsAsync(defaultRole))
                    {
                        var role = new TIdentityRole
                        {
                            Name = defaultRole
                        };

                        var roleResult = await roleManager.CreateAsync(role);
                        if (!roleResult.Succeeded)
                        {
                            throw new ApplicationException($"Could not create '{defaultRole}' role");
                        }
                    }
                }
                catch (Exception e)
                {
                    //Helper.Log(e.ToString());
                }
            }
        }

        private static async Task<TIdentityUser> CreateDefaultAdminUser(UserManager<TIdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync(DefaultAdminUserEmail);

            try
            {
                if (user == null)
                {
                    user = new TIdentityUser
                    {
                        //UserNumber = 1,
                        UserName = "saiful@gmail.com",
                        Email = DefaultAdminUserEmail,
                        EmailConfirmed = true,
                        Region = new Region { RegionName = "Bangladesh" }
                        //Designation = Designation.SuperAdmin,
                        //FullName = "CutOutWiz",
                        //DefaultPasswordChanged = true
                    };
                    var userResult = await userManager.CreateAsync(user, DefaultAdminUserPassword);

                    if (!userResult.Succeeded)
                    {
                        throw new ApplicationException($"Could not create '{DefaultAdminUserEmail}' user");
                    }
                }
            }
            catch (Exception e)
            {
                //Helper.Log(e.ToString());
            }

            return user;
        }

        private static async Task AddDefaultAdminRoleToDefaultAdminUser(
            UserManager<TIdentityUser> userManager,
            TIdentityUser user)
        {
            // Add user to Administrator role if it's not already associated
            if (!(await userManager.GetRolesAsync(user)).Contains(DefaultAdminRoleName))
            {
                var addToRoleResult = await userManager.AddToRoleAsync(user, DefaultAdminRoleName);
                if (!addToRoleResult.Succeeded)
                {
                    throw new ApplicationException(
                        $"Could not add user '{DefaultAdminUserEmail}' to '{DefaultAdminRoleName}' role");
                }
            }
        }

        public static async Task SeedDataAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<TIdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<TIdentityRole>>();

            //await context.Database.EnsureCreatedAsync(); //This causes migration error

            await CreateDefaultRoles(roleManager);
            var defaultAdminUser = await CreateDefaultAdminUser(userManager);

            await AddDefaultAdminRoleToDefaultAdminUser(userManager, defaultAdminUser);
            SaveInitData(context);
        }

        private static void SaveInitData(ApplicationDbContext context)
        {
            if (context.Activities.Count() > 0) return;

            IUserActivityService _userActivityService = new UserActivityService(context);

            RecretionalActivity kayaking = new RecretionalActivity { ActivityName = "Kayaking" };
            _userActivityService.AddAnActivity(kayaking);
            RecretionalActivity camping = new RecretionalActivity { ActivityName = "Camping" };
            _userActivityService.AddAnActivity(camping);
            RecretionalActivity fishing = new RecretionalActivity { ActivityName = "Fishing" };
            _userActivityService.AddAnActivity(fishing);
            RecretionalActivity hiking = new RecretionalActivity { ActivityName = "Hiking" };
            _userActivityService.AddAnActivity(hiking);
            RecretionalActivity sail = new RecretionalActivity { ActivityName = "Sailing" };
            _userActivityService.AddAnActivity(sail);
            RecretionalActivity biking = new RecretionalActivity { ActivityName = "Biking" };
            _userActivityService.AddAnActivity(biking);
            //////////////////////
            Region japan = new Region { RegionName = "Japan" };
            Region usa = new Region { RegionName = "USA" };
            Region canada = new Region { RegionName = "Canada" };
            Region turk = new Region { RegionName = "Turkey" };
            Region india = new Region { RegionName = "India" };
            Region pak = new Region { RegionName = "Pakistan" };
            Region nepal = new Region { RegionName = "Nepal" };
            
            _userActivityService.AddRegion(japan);
            _userActivityService.AddRegion(usa);
            _userActivityService.AddRegion(canada);
            _userActivityService.AddRegion(turk);
            _userActivityService.AddRegion(india);

            ApplicationUser safa = new ApplicationUser { UserName = "safa@gmail.com", Region = japan, DateOfBirth = new DateTime(2000, 01, 01) };
            ApplicationUser alif = new ApplicationUser { UserName = "alif@gmail.com", Region = japan, DateOfBirth = new DateTime(2001, 01, 01) };
            ApplicationUser hamza = new ApplicationUser { UserName = "hamza@gmail.com", Region = japan, DateOfBirth = new DateTime(2002, 01, 01) };
            ApplicationUser kabir = new ApplicationUser { UserName = "kabir@gmail.com", Region = japan, DateOfBirth = new DateTime(1990, 01, 01) };
            ApplicationUser saif = new ApplicationUser { UserName = "saif@gmail.com", Region = canada, DateOfBirth = new DateTime(1998, 01, 01) };
            ApplicationUser salman = new ApplicationUser { UserName = "salman@gmail.com", Region = canada, DateOfBirth = new DateTime(1995, 01, 01) };
            ApplicationUser rubel = new ApplicationUser { UserName = "rubel@gmail.com", Region = canada, DateOfBirth = new DateTime(1992, 01, 01) };
            ApplicationUser talha = new ApplicationUser { UserName = "talha@gmail.com", Region = usa, DateOfBirth = new DateTime(1980, 01, 01) };
            ApplicationUser rahim = new ApplicationUser { UserName = "rahim@gmail.com", Region = usa, DateOfBirth = new DateTime(1985, 01, 01) };
            ApplicationUser karim = new ApplicationUser { UserName = "karim@gmail.com", Region = pak, DateOfBirth = new DateTime(1970, 01, 01) };
            ApplicationUser polash = new ApplicationUser { UserName = "polash@gmail.com", Region = nepal, DateOfBirth = new DateTime(1988, 01, 01) };
            ApplicationUser kamal = new ApplicationUser { UserName = "kamal@gmail.com", Region = nepal, DateOfBirth = new DateTime(1978, 01, 01) };

            _userActivityService.AddUser(safa);
            _userActivityService.AddUser(alif);
            _userActivityService.AddUser(hamza);
            _userActivityService.AddUser(kabir);
            _userActivityService.AddUser(saif);
            _userActivityService.AddUser(salman);
            _userActivityService.AddUser(rubel);
            _userActivityService.AddUser(talha);
            _userActivityService.AddUser(rahim);
            _userActivityService.AddUser(karim);
            _userActivityService.AddUser(polash);
            _userActivityService.AddUser(kamal);

            _userActivityService.ChooseAnActivity(safa, kayaking);
            _userActivityService.ChooseAnActivity(safa, camping);
            _userActivityService.ChooseAnActivity(safa, fishing);

            _userActivityService.ChooseAnActivity(alif, kayaking);
            _userActivityService.ChooseAnActivity(alif, fishing);
            _userActivityService.ChooseAnActivity(alif, biking);

            _userActivityService.ChooseAnActivity(kamal, biking);
            _userActivityService.ChooseAnActivity(kamal, hiking);
            _userActivityService.ChooseAnActivity(kamal, sail);

            _userActivityService.ChooseAnActivity(kabir, biking);
            _userActivityService.ChooseAnActivity(kabir, camping);
            _userActivityService.ChooseAnActivity(kabir, sail);

            _userActivityService.ChooseAnActivity(polash, hiking);
            _userActivityService.ChooseAnActivity(polash, kayaking);

            _userActivityService.ChooseAnActivity(karim, biking);
            _userActivityService.ChooseAnActivity(karim, fishing);

            _userActivityService.SetRankAnActivity(safa, kayaking, Rating.fiveStar);
            _userActivityService.SetRankAnActivity(safa, camping, Rating.fourStar);
            _userActivityService.SetRankAnActivity(safa, fishing, Rating.threeStar);

            _userActivityService.SetRankAnActivity(alif, kayaking, Rating.threeStar);
            _userActivityService.SetRankAnActivity(alif, fishing, Rating.twoStar);
            _userActivityService.SetRankAnActivity(alif, biking, Rating.oneStar);

            _userActivityService.SetRankAnActivity(kamal, biking, Rating.threeStar);
            _userActivityService.SetRankAnActivity(kamal, sail, Rating.twoStar);
            _userActivityService.SetRankAnActivity(kamal, hiking, Rating.oneStar);

            _userActivityService.SetRankAnActivity(kabir, biking, Rating.threeStar);
            _userActivityService.SetRankAnActivity(kabir, camping, Rating.twoStar);
            _userActivityService.SetRankAnActivity(kabir, sail, Rating.oneStar);

            _userActivityService.SetRankAnActivity(polash, hiking, Rating.fourStar);
            _userActivityService.SetRankAnActivity(polash, kayaking, Rating.twoStar);

            _userActivityService.SetRankAnActivity(karim, biking, Rating.twoStar);
            _userActivityService.SetRankAnActivity(karim, fishing, Rating.fiveStar);
        }
    }
}
