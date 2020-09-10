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
        bool AddRegion(Region region);
        bool AddUser(ApplicationUser user);
        IEnumerable<RecretionalActivity> GetActivities();
        IEnumerable<UserActivityRanking> GetAllUserActivities();
        bool CanChooseUptoThreeActivities(ApplicationUser user);
        bool SetRankAnActivity(ApplicationUser user, RecretionalActivity activity, Rating rating);
        bool ChooseAnActivity(ApplicationUser user, RecretionalActivity activity);
        bool AddAnActivity(RecretionalActivity activity);
        RecretionalActivity GetActivityById(int activityId);
        bool UpdateActivity(RecretionalActivity activity);
        bool DeleteActivity(int activityId);
        //RecretionalActivity GetTopOneRankActivity();
        List<RecretionalActivityViewModel> GetTopFiveRankActivity();
        //Region GetTopOneRegionChoosenByTheRegisteredUsers();
        List<RegionViewModel> GetTopFiveRegionChoosenByTheRegisteredUsers();
        List<TopUserViewModel> GetTopUsersChoosenByAgeGroup();
        UserActivityRanking GetUserActivityById(string userId, int activityId);
    }
}
