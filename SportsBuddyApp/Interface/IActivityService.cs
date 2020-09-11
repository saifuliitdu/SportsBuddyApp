using SportsBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddyApp.Interface
{
    public interface IActivityService
    {
        bool AddAnActivity(RecretionalActivity activity);
        RecretionalActivity GetActivityById(int Id);
        IEnumerable<RecretionalActivity> GetAllActivities();
        bool UpdateActivity(RecretionalActivity activity);
        bool DeleteActivity(int Id);
    }
}
