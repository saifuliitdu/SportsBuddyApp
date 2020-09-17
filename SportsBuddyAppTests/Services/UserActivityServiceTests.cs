using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportsBuddy.Data;
using SportsBuddy.Models;
using SportsBuddyApp.Interface;
using SportsBuddyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsBuddyApp.Services.Tests
{
    public class UserActivityServiceTests
    {
        ApplicationDbContext _context;
        [SetUp]
        public void SetUp()
        {
            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //   .UseSqlServer("Data Source=(local)\\SQLEXPRESS01;Initial Catalog=SportsBuddyDB;User ID=sa;Password=1234").Options;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("testdb").Options;
            _context = new ApplicationDbContext(options);
        }

        [Test()]
        public void SelectUptoThreeActivities_Return_True_Test()
        {
            IUserService _userService = new UserService(_context);
            IActivityService _activityService = new ActivityService(_context);
            IUserActivityService _userActivityService = new UserActivityService(_context);
            ApplicationUser user = new ApplicationUser { UserName = "test@gmail.com" };
            bool userSaveResult = _userService.AddUser(user);

            RecretionalActivity kayaking = new RecretionalActivity { ActivityName = "Kayaking" };
            _activityService.AddAnActivity(kayaking);
            RecretionalActivity camping = new RecretionalActivity { ActivityName = "Camping" };
            _activityService.AddAnActivity(camping);
            RecretionalActivity fishing = new RecretionalActivity { ActivityName = "Fishing" };
            _activityService.AddAnActivity(fishing);
            RecretionalActivity golfing = new RecretionalActivity { ActivityName = "Golfing" };
            _activityService.AddAnActivity(golfing);

            if (userSaveResult)
            {
                _userActivityService.ChooseAnActivity(user, kayaking);
                _userActivityService.ChooseAnActivity(user, camping);
                _userActivityService.ChooseAnActivity(user, fishing);
                _userActivityService.ChooseAnActivity(user, golfing);
            }

            bool selectUpToThreeActivities = _userActivityService.CanChooseUptoThreeActivities(user);

            Assert.IsTrue(selectUpToThreeActivities);
        }

        [Test()]
        public void SelectUptoThreeActivities_Below_Three_Return_False_Test()
        {
            IUserService _userService = new UserService(_context);
            IActivityService _activityService = new ActivityService(_context);
            IUserActivityService _userActivityService = new UserActivityService(_context);
            ApplicationUser user = new ApplicationUser { UserName = "test@gmail.com" };
            bool userSaveResult = _userService.AddUser(user);

            RecretionalActivity kayaking = new RecretionalActivity { ActivityName = "Kayaking" };
            _activityService.AddAnActivity(kayaking);
            RecretionalActivity camping = new RecretionalActivity { ActivityName = "Camping" };
            _activityService.AddAnActivity(camping);

            if (userSaveResult)
            {
                _userActivityService.ChooseAnActivity(user, kayaking);
                _userActivityService.ChooseAnActivity(user, camping);
            }

            bool selectUpToThreeActivities = _userActivityService.CanChooseUptoThreeActivities(user);

            Assert.IsFalse(selectUpToThreeActivities);
        }
        [Test()]
        public void SetRankAnActivityTest()
        {
            IUserService _userService = new UserService(_context);
            IActivityService _activityService = new ActivityService(_context);
            IUserActivityService _userActivityService = new UserActivityService(_context);
            ApplicationUser user = new ApplicationUser { UserName = "test@gmail.com" };
            bool userSaveResult = _userService.AddUser(user);

            RecretionalActivity kayaking = new RecretionalActivity { ActivityName = "Kayaking" };
            _activityService.AddAnActivity(kayaking);
            RecretionalActivity camping = new RecretionalActivity { ActivityName = "Camping" };
            _activityService.AddAnActivity(camping);

            if (userSaveResult)
            {
                _userActivityService.ChooseAnActivity(user, kayaking);
                _userActivityService.ChooseAnActivity(user, camping);
            }

            bool setKayakingAsFiveStarRatingResult = _userActivityService.SetRankAnActivity(user, kayaking, Rating.fiveStar);
            bool setCampingAsFourStarRatingResult = _userActivityService.SetRankAnActivity(user, camping, Rating.fourStar);
            Assert.IsTrue(setKayakingAsFiveStarRatingResult);
            Assert.IsTrue(setCampingAsFourStarRatingResult);
        }

        [Test()]
        public void GetTopOneRankActivityTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("testdb1").Options;
            _context = new ApplicationDbContext(options);
            IUserService _userService = new UserService(_context);
            IActivityService _activityService = new ActivityService(_context);
            IUserActivityService _userActivityService = new UserActivityService(_context);

            ApplicationUser faisal = new ApplicationUser { UserName = "faisal@gmail.com" };
            _userService.AddUser(faisal);
            ApplicationUser jamil = new ApplicationUser { UserName = "jamil@gmail.com" };
            _userService.AddUser(jamil);

            RecretionalActivity carRaicing = new RecretionalActivity { ActivityName = "Car Raicing" };
            _activityService.AddAnActivity(carRaicing);
            RecretionalActivity snowSkiing = new RecretionalActivity { ActivityName = "Snow Skiing" };
            _activityService.AddAnActivity(snowSkiing);
            RecretionalActivity golfing = new RecretionalActivity { ActivityName = "Golfing" };
            _activityService.AddAnActivity(golfing);
            //////////////////////



            _userActivityService.ChooseAnActivity(faisal, carRaicing);
            _userActivityService.ChooseAnActivity(faisal, snowSkiing);
            _userActivityService.ChooseAnActivity(faisal, golfing);

            _userActivityService.ChooseAnActivity(jamil, carRaicing);
            _userActivityService.ChooseAnActivity(jamil, golfing);
            _userActivityService.ChooseAnActivity(jamil, snowSkiing);

            _userActivityService.SetRankAnActivity(faisal, carRaicing, Rating.fiveStar);
            _userActivityService.SetRankAnActivity(faisal, snowSkiing, Rating.fourStar);
            _userActivityService.SetRankAnActivity(faisal, golfing, Rating.threeStar);

            _userActivityService.SetRankAnActivity(jamil, carRaicing, Rating.threeStar);
            _userActivityService.SetRankAnActivity(jamil, snowSkiing, Rating.twoStar);
            _userActivityService.SetRankAnActivity(jamil, golfing, Rating.oneStar);

            var topActivity = _userActivityService.GetTopOneRankActivity();
            Assert.IsNotNull(topActivity);
            Assert.AreEqual(carRaicing.ActivityName, topActivity.ActivityName);
        }

        [Test()]
        public void GetTopFiveRankActivityTest()
        {
            IUserService _userService = new UserService(_context);
            IActivityService _activityService = new ActivityService(_context);
            IUserActivityService _userActivityService = new UserActivityService(_context);

            RecretionalActivity kayaking = new RecretionalActivity { ActivityName = "Kayaking" };
            _activityService.AddAnActivity(kayaking);
            RecretionalActivity camping = new RecretionalActivity { ActivityName = "Camping" };
            _activityService.AddAnActivity(camping);
            RecretionalActivity fishing = new RecretionalActivity { ActivityName = "Fishing" };
            _activityService.AddAnActivity(fishing);
            RecretionalActivity hiking = new RecretionalActivity { ActivityName = "Hiking" };
            _activityService.AddAnActivity(hiking);
            RecretionalActivity sail = new RecretionalActivity { ActivityName = "Sailing" };
            _activityService.AddAnActivity(sail);
            RecretionalActivity biking = new RecretionalActivity { ActivityName = "Biking" };
            _activityService.AddAnActivity(biking);
            //////////////////////
            ApplicationUser saiful = new ApplicationUser { UserName = "saiful@gmail.com" };
            _userService.AddUser(saiful);
            ApplicationUser alif = new ApplicationUser { UserName = "alif@gmail.com" };
            _userService.AddUser(alif);
            ApplicationUser kamal = new ApplicationUser { UserName = "kamal@gmail.com" };
            _userService.AddUser(kamal);
            ApplicationUser jamal = new ApplicationUser { UserName = "jamal@gmail.com" };
            _userService.AddUser(jamal);

            _userActivityService.ChooseAnActivity(saiful, kayaking);
            _userActivityService.ChooseAnActivity(saiful, camping);
            _userActivityService.ChooseAnActivity(saiful, fishing);

            _userActivityService.ChooseAnActivity(alif, kayaking);
            _userActivityService.ChooseAnActivity(alif, fishing);
            _userActivityService.ChooseAnActivity(alif, biking);

            _userActivityService.ChooseAnActivity(kamal, biking);
            _userActivityService.ChooseAnActivity(kamal, hiking);
            _userActivityService.ChooseAnActivity(kamal, sail);

            _userActivityService.ChooseAnActivity(jamal, biking);
            _userActivityService.ChooseAnActivity(jamal, camping);
            _userActivityService.ChooseAnActivity(jamal, sail);

            _userActivityService.SetRankAnActivity(saiful, kayaking, Rating.fiveStar);
            _userActivityService.SetRankAnActivity(saiful, camping, Rating.fourStar);
            _userActivityService.SetRankAnActivity(saiful, fishing, Rating.threeStar);

            _userActivityService.SetRankAnActivity(alif, kayaking, Rating.threeStar);
            _userActivityService.SetRankAnActivity(alif, fishing, Rating.twoStar);
            _userActivityService.SetRankAnActivity(alif, biking, Rating.oneStar);

            _userActivityService.SetRankAnActivity(kamal, biking, Rating.threeStar);
            _userActivityService.SetRankAnActivity(kamal, sail, Rating.twoStar);
            _userActivityService.SetRankAnActivity(kamal, hiking, Rating.oneStar);

            _userActivityService.SetRankAnActivity(jamal, biking, Rating.threeStar);
            _userActivityService.SetRankAnActivity(jamal, camping, Rating.twoStar);
            _userActivityService.SetRankAnActivity(jamal, sail, Rating.oneStar);

            // kayaking = 5 + 3 = 8, campinig = 4+2=6, fishing=3+2=5, biking=1+3+3=7
            // hiking = 2, sailing=2+1=3,golging= 1


            var topActivity = _userActivityService.GetTopFiveRankActivity();
            Assert.IsNotNull(topActivity);
            Assert.AreEqual(kayaking.ActivityName, topActivity.FirstOrDefault().Activity.ActivityName);
            Assert.AreEqual(biking.ActivityName, topActivity.Skip(1).FirstOrDefault().Activity.ActivityName);
            Assert.AreEqual(camping.ActivityName, topActivity.Skip(2).FirstOrDefault().Activity.ActivityName);
            Assert.AreEqual(fishing.ActivityName, topActivity.Skip(3).FirstOrDefault().Activity.ActivityName);
            Assert.AreEqual(sail.ActivityName, topActivity.LastOrDefault().Activity.ActivityName);
        }

        [Test()]
        public void GetTopOneRegionChoosenByTheRegisteredUsersTest()
        {
            IUserService _userService = new UserService(_context);
            IActivityService _activityService = new ActivityService(_context);
            IRegionService _regionService = new RegionService(_context);
            IUserActivityService _userActivityService = new UserActivityService(_context);
            // Region
            Region bd = new Region { RegionName = "Bangladesh" };
            Region usa = new Region { RegionName = "USA" };
            Region canada = new Region { RegionName = "Canada" };
            _regionService.AddRegion(bd);
            _regionService.AddRegion(usa);
            _regionService.AddRegion(canada);

            ApplicationUser test = new ApplicationUser { UserName = "test@gmail.com", Region = bd };
            ApplicationUser alif = new ApplicationUser { UserName = "alif@gmail.com", Region = usa };
            ApplicationUser hamza = new ApplicationUser { UserName = "hamza@gmail.com", Region = canada };
            ApplicationUser kabir = new ApplicationUser { UserName = "kabir@gmail.com", Region = bd };
            ApplicationUser saif = new ApplicationUser { UserName = "saif@gmail.com", Region = canada };
            ApplicationUser salman = new ApplicationUser { UserName = "kabir@gmail.com", Region = bd };
            _userService.AddUser(test);
            _userService.AddUser(alif);
            _userService.AddUser(hamza);
            _userService.AddUser(kabir);
            _userService.AddUser(saif);
            _userService.AddUser(salman);



            var topRegion = _userActivityService.GetTopFiveRegionChoosenByTheRegisteredUsers().FirstOrDefault();
            Assert.IsNotNull(topRegion);
            Assert.AreEqual(bd.RegionName, topRegion.Region.RegionName);
        }

        [Test()]
        public void GetTopFiveRegionChoosenByTheRegisteredUsersTest()
        {
            IUserService _userService = new UserService(_context);
            IActivityService _activityService = new ActivityService(_context);
            IUserActivityService _userActivityService = new UserActivityService(_context);
            IRegionService _regionService = new RegionService(_context);
            // Region
            Region bd = new Region { RegionName = "Bangladesh" };
            Region usa = new Region { RegionName = "USA" };
            Region canada = new Region { RegionName = "Canada" };
            Region nepal = new Region { RegionName = "Nepal" };
            Region pak = new Region { RegionName = "Pakistan" };
            Region bhutan = new Region { RegionName = "Bhutan" };
            Region india = new Region { RegionName = "India" };
            Region china = new Region { RegionName = "China" };
            _regionService.AddRegion(bd);
            _regionService.AddRegion(usa);
            _regionService.AddRegion(canada);
            _regionService.AddRegion(nepal);
            _regionService.AddRegion(pak);
            _regionService.AddRegion(india);
            _regionService.AddRegion(bhutan);
            _regionService.AddRegion(china);

            // User
            ApplicationUser safa = new ApplicationUser { UserName = "safa@gmail.com", Region = bd };
            ApplicationUser alif = new ApplicationUser { UserName = "alif@gmail.com", Region = bd };
            ApplicationUser hamza = new ApplicationUser { UserName = "hamza@gmail.com", Region = bd };
            ApplicationUser kabir = new ApplicationUser { UserName = "kabir@gmail.com", Region = bd };
            ApplicationUser saif = new ApplicationUser { UserName = "saif@gmail.com", Region = canada };
            ApplicationUser salman = new ApplicationUser { UserName = "kabir@gmail.com", Region = canada };
            ApplicationUser rubel = new ApplicationUser { UserName = "rubel@gmail.com", Region = canada };
            ApplicationUser talha = new ApplicationUser { UserName = "talha@gmail.com", Region = usa };
            ApplicationUser rahim = new ApplicationUser { UserName = "rahim@gmail.com", Region = usa };
            ApplicationUser karim = new ApplicationUser { UserName = "karim@gmail.com", Region = pak };
            ApplicationUser polash = new ApplicationUser { UserName = "polash@gmail.com", Region = bhutan };
            ApplicationUser kamal = new ApplicationUser { UserName = "kamal@gmail.com", Region = nepal };

            _userService.AddUser(safa);
            _userService.AddUser(alif);
            _userService.AddUser(hamza);
            _userService.AddUser(kabir);
            _userService.AddUser(saif);
            _userService.AddUser(salman);
            _userService.AddUser(rubel);
            _userService.AddUser(talha);
            _userService.AddUser(rahim);
            _userService.AddUser(karim);
            _userService.AddUser(polash);
            _userService.AddUser(kamal);

            var topFiveRegions = _userActivityService.GetTopFiveRegionChoosenByTheRegisteredUsers();
            Assert.IsNotNull(topFiveRegions);
            Assert.AreEqual(bd.RegionName, topFiveRegions.FirstOrDefault().Region.RegionName);
            Assert.AreEqual(canada.RegionName, topFiveRegions.Skip(1).FirstOrDefault().Region.RegionName);
            Assert.AreEqual(usa.RegionName, topFiveRegions.Skip(2).FirstOrDefault().Region.RegionName);
        }

        [Test()]
        public void GetTopUsersChoosenByAgeGroupTest()
        {
            IUserService _userService = new UserService(_context);
            IActivityService _activityService = new ActivityService(_context);
            IRegionService _regionService = new RegionService(_context);
            IUserActivityService _userActivityService = new UserActivityService(_context);
            // Region
            Region bd = new Region { RegionName = "Bangladesh" };
            Region usa = new Region { RegionName = "USA" };
            Region canada = new Region { RegionName = "Canada" };
            Region nepal = new Region { RegionName = "Nepal" };
            Region pak = new Region { RegionName = "Pakistan" };
            Region bhutan = new Region { RegionName = "Bhutan" };
            Region india = new Region { RegionName = "India" };
            Region china = new Region { RegionName = "China" };
            _regionService.AddRegion(bd);
            _regionService.AddRegion(usa);
            _regionService.AddRegion(canada);
            _regionService.AddRegion(nepal);
            _regionService.AddRegion(pak);
            _regionService.AddRegion(india);
            _regionService.AddRegion(bhutan);
            _regionService.AddRegion(china);

            // User
            ApplicationUser safa = new ApplicationUser { UserName = "safa@gmail.com", Region = bd, DateOfBirth = new DateTime(2000, 01, 01) };
            ApplicationUser alif = new ApplicationUser { UserName = "alif@gmail.com", Region = bd, DateOfBirth = new DateTime(2001, 01, 01) };
            ApplicationUser hamza = new ApplicationUser { UserName = "hamza@gmail.com", Region = bd, DateOfBirth = new DateTime(2002, 01, 01) };
            ApplicationUser kabir = new ApplicationUser { UserName = "kabir@gmail.com", Region = bd, DateOfBirth = new DateTime(1990, 01, 01) };
            ApplicationUser saif = new ApplicationUser { UserName = "saif@gmail.com", Region = canada, DateOfBirth = new DateTime(1998, 01, 01) };
            ApplicationUser salman = new ApplicationUser { UserName = "kabir@gmail.com", Region = canada, DateOfBirth = new DateTime(1995, 01, 01) };
            ApplicationUser rubel = new ApplicationUser { UserName = "rubel@gmail.com", Region = canada, DateOfBirth = new DateTime(1992, 01, 01) };
            ApplicationUser talha = new ApplicationUser { UserName = "talha@gmail.com", Region = usa, DateOfBirth = new DateTime(1980, 01, 01) };
            ApplicationUser rahim = new ApplicationUser { UserName = "rahim@gmail.com", Region = usa, DateOfBirth = new DateTime(1985, 01, 01) };
            ApplicationUser karim = new ApplicationUser { UserName = "karim@gmail.com", Region = pak, DateOfBirth = new DateTime(1970, 01, 01) };
            ApplicationUser polash = new ApplicationUser { UserName = "polash@gmail.com", Region = bhutan, DateOfBirth = new DateTime(1988, 01, 01) };
            ApplicationUser kamal = new ApplicationUser { UserName = "kamal@gmail.com", Region = nepal, DateOfBirth = new DateTime(1978, 01, 01) };

            _userService.AddUser(safa);
            _userService.AddUser(alif);
            _userService.AddUser(hamza);
            _userService.AddUser(kabir);
            _userService.AddUser(saif);
            _userService.AddUser(salman);
            _userService.AddUser(rubel);
            _userService.AddUser(talha);
            _userService.AddUser(rahim);
            _userService.AddUser(karim);
            _userService.AddUser(polash);
            _userService.AddUser(kamal);

            var topUsers = _userActivityService.GetTopUsersChoosenByAgeGroup();
            Assert.IsNotNull(topUsers);
            Assert.AreEqual(5, topUsers.FirstOrDefault(f => f.AgeGroup.Equals("18 - 25")).Count);
        }

        [Test()]
        public void ChooseAnActivityTest()
        {
            IUserService _userService = new UserService(_context);
            IActivityService _activityService = new ActivityService(_context);
            IUserActivityService _userActivityService = new UserActivityService(_context);
            ApplicationUser user = new ApplicationUser { UserName = "test@gmail.com" };
            bool userSaveResult = _userService.AddUser(user);

            RecretionalActivity activity = new RecretionalActivity { ActivityName = "Kayaking" };
            bool activitySaveResult = _activityService.AddAnActivity(activity);

            bool selectAnActivityResult = false;
            if (userSaveResult && activitySaveResult)
            {
                selectAnActivityResult = _userActivityService.ChooseAnActivity(user, activity);
            }

            Assert.IsTrue(selectAnActivityResult);
        }

        [Test()]
        public void AddUserTest()
        {
            IUserService _userService = new UserService(_context);
            ApplicationUser user = new ApplicationUser { UserName = "test@gmail.com" };

            bool result = _userService.AddUser(user);
            Assert.IsTrue(result);
        }
        [Test()]
        public void AddActivityTest()
        {
            IActivityService _activityService = new ActivityService(_context);
            RecretionalActivity activity = new RecretionalActivity { ActivityName = "Kayaking" };

            bool result = _activityService.AddAnActivity(activity);
            Assert.IsTrue(result);
        }
    }
}