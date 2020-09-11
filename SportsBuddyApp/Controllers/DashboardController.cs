using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsBuddy.Data;
using SportsBuddyApp.Models.ViewModel;
using SportsBuddyApp.Interface;
using SportsBuddy.Models;

namespace CutOutWizWebApp.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,User")]
    public class DashboardController : Controller
    {
        private readonly IUserActivityService _userActivityService;
        public DashboardController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }
        public IActionResult Index()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.TopFiveActivities = _userActivityService.GetTopFiveRankActivity();
            dashboardViewModel.TopFiveRegions = _userActivityService.GetTopFiveRegionChoosenByTheRegisteredUsers();
            dashboardViewModel.TopUserGroups = _userActivityService.GetTopUsersChoosenByAgeGroup();

            return View(dashboardViewModel);
        }
    }
}