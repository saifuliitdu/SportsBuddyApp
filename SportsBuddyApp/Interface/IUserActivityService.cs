using SportsBuddy.Data;
using SportsBuddy.Models;
using SportsBuddyApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddyApp.Interface
{
    public interface IUserActivityService
    {
        IEnumerable<UserActivityRanking> GetAllUserActivities();
        bool CanChooseUptoThreeActivities(ApplicationUser user);
        bool SetRankAnActivity(ApplicationUser user, RecretionalActivity activity, Rating rating);
        bool ChooseAnActivity(ApplicationUser user, RecretionalActivity activity);
        List<RecretionalActivityViewModel> GetTopFiveRankActivity();
        List<RegionViewModel> GetTopFiveRegionChoosenByTheRegisteredUsers();
        List<TopUserViewModel> GetTopUsersChoosenByAgeGroup();
        UserActivityRanking GetUserActivityById(string userId, int activityId);
    }
}
