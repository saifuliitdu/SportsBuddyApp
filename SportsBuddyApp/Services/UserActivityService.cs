using SportsBuddy.Data;
using SportsBuddy.Models;
using SportsBuddyApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsBuddyApp.Models.ViewModel;

namespace SportsBuddyApp.Services
{
    public class UserActivityService : IUserActivityService
    {
        ApplicationDbContext _context;
        public UserActivityService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddRegion(Region region)
        {
            _context.Regions.Add(region);
            return SaveChanges();
        }
        public bool AddUser(ApplicationUser user)
        {
            _context.Users.Add(user);
            return SaveChanges();
        }
        public bool AddAnActivity(RecretionalActivity activity)
        {
            _context.Activities.Add(activity);
            return SaveChanges();
        }
        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<RecretionalActivity> GetActivities()
        {
            return _context.Activities.ToList();
        }
        public RecretionalActivity GetActivityById(int activityId)
        {
            var activity = _context.Activities.FirstOrDefault(f => f.ActivityId == activityId);
            return activity;
        }
        public bool UpdateActivity(RecretionalActivity activity)
        {
            var existingActivity = GetActivityById(activity.ActivityId);
            if (existingActivity != null)
                existingActivity.ActivityName = activity.ActivityName;
            return SaveChanges();
        }
        public bool DeleteActivity(int activityId)
        {
            var existingActivity = GetActivityById(activityId);
            if (existingActivity != null)
                _context.Activities.Remove(existingActivity);
            return SaveChanges();
        }
        public bool CanChooseUptoThreeActivities(ApplicationUser user)
        {
            var userActivityCount = _context.UserActivityRankings
                .Include(p => p.User)
                .Count(x => x.User.Id == user.Id);
            return userActivityCount >= 3;
        }

        public bool ChooseAnActivity(ApplicationUser user, RecretionalActivity activity)
        {
            if (!CanChooseUptoThreeActivities(user))
            {
                UserActivityRanking userActivityRanking = new UserActivityRanking { User = user, Activity = activity };
                _context.UserActivityRankings.Add(userActivityRanking);
                return SaveChanges();
            }
            return false;
        }

        public bool SetRankAnActivity(ApplicationUser user, RecretionalActivity activity, Rating rating)
        {
            var userActivity = _context.UserActivityRankings
                .Include(p => p.User)
                .Include(p => p.Activity)
                //.Include(p => p.Rating)
                .FirstOrDefault(x => x.User.Id == user.Id && x.Activity.ActivityId == activity.ActivityId);

            userActivity.Rating = rating;
            return SaveChanges();
        }

        public RecretionalActivity GetTopOneRankActivity()
        {
            var userActivityRankings = _context.UserActivityRankings
                .Include(p => p.User)
                .Include(p => p.Activity)
                .ToList();

            var topOneActivityGroup = userActivityRankings.GroupBy(g => g.ActivityId).Select(x => new { Key = x.Key, Sum = x.Sum(y => (int)y.Rating) }).OrderByDescending(p => p.Sum).FirstOrDefault();

            var topActivity = userActivityRankings.FirstOrDefault(f => f.ActivityId == topOneActivityGroup.Key);

            return topActivity.Activity;
        }

        public List<RecretionalActivityViewModel> GetTopFiveRankActivity()
        {
            var userActivityRankings = _context.UserActivityRankings
                .Include(p => p.User)
                .Include(p => p.Activity)
                .ToList();

            var topFiveKeyRankSumList = userActivityRankings.GroupBy(g => g.ActivityId).Select(x => new { Key = x.Key, Sum = x.Sum(y => (int)y.Rating) }).OrderByDescending(p => p.Sum).Take(5).ToList();
            var topFoiveActivity = new List<RecretionalActivityViewModel>();
            topFiveKeyRankSumList.ForEach(x =>
            {
                RecretionalActivityViewModel activityViewModel = new RecretionalActivityViewModel();
                var obj = userActivityRankings.FirstOrDefault(f => f.ActivityId == x.Key);
                if (obj != null)
                {
                    activityViewModel.Activity = obj.Activity;
                    activityViewModel.Rating = x.Sum;
                    topFoiveActivity.Add(activityViewModel);
                }
            });

            return topFoiveActivity;
        }
        public Region GetTopOneRegionChoosenByTheRegisteredUsers()
        {
            return _context.Regions.Include(x => x.Users).OrderByDescending(x => x.Users.Count).FirstOrDefault();
        }
        public List<RegionViewModel> GetTopFiveRegionChoosenByTheRegisteredUsers()
        {
            List<RegionViewModel> regionList = new List<RegionViewModel>();

            regionList = _context.Regions.Include(x => x.Users).Select(y => new RegionViewModel { Region = y, UserCount = y.Users.Count }).ToList().OrderByDescending(p => p.UserCount).Take(5).ToList();

            return regionList;
        }

        public List<TopUserViewModel> GetTopUsersChoosenByAgeGroup()
        {
            var topUsers = _context.Users.Where(p => p.DateOfBirth != null).ToList()
                .Select(p => new { UserId = p.Id, Age = DateTime.Now.Year - p.DateOfBirth.Year })
                .GroupBy(p => (int)((p.Age - 18) / 10))
                .Select(g => new TopUserViewModel { AgeGroup = string.Format("{0} - {1}", g.Key * 10 + 18, g.Key * 10 + 25), Count = g.Count() }).ToList();
            return topUsers;
        }

        public IEnumerable<UserActivityRanking> GetAllUserActivities()
        {
            var allUserActivities = _context.UserActivityRankings.Include(x => x.User).Include(x => x.Activity).ToList();
            return allUserActivities;
        }

        public UserActivityRanking GetUserActivityById(string userId, int activityId)
        {
            var userActivity = _context.UserActivityRankings.Include(x => x.User).Include(x => x.Activity)
                .FirstOrDefault(f=>f.UserId == userId && f.ActivityId == activityId);
            return userActivity;
        }
    }
}
