using SportsBuddy.Data;
using SportsBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddyApp.Models.ViewModel
{
    public class DashboardViewModel
    {
        public List<RecretionalActivityViewModel> TopFiveActivities { get; set; }
        public List<TopUserViewModel> TopUserGroups { get; set; }
        public List<RegionViewModel> TopFiveRegions { get; set; }
        public DashboardViewModel()
        {
            TopFiveActivities = new List<RecretionalActivityViewModel>();
            TopFiveRegions = new List<RegionViewModel>();
            TopUserGroups = new List<TopUserViewModel>();
        }
    }
}
