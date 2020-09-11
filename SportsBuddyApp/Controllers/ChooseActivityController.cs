using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsBuddy.Data;
using SportsBuddyApp.Models.ViewModel;
using SportsBuddyApp.Interface;
using SportsBuddy.Models;
using System;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CutOutWizWebApp.Controllers
{
    [Authorize]
    public class ChooseActivityController : Controller
    {
        private readonly IUserActivityService _userActivityService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChooseActivityController(IUserActivityService userActivityService, UserManager<ApplicationUser> userManager)
        {
            _userActivityService = userActivityService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var allUserActivities = _userActivityService.GetAllUserActivities();

            var user = _userManager.GetUserAsync(User).Result;
            var allActivities = allUserActivities.Where(x => x.User.Id == user.Id)
                .Select(y => new ChooseActivityViewModel { Activity = y.Activity, IsChoosen = y.User.Id == user.Id }).ToList();

            var otherActivities = allUserActivities.GroupBy(g => g.ActivityId).Select(y =>
               !allActivities.Select(x => x.Activity.ActivityId).ToList().Contains(y.FirstOrDefault().ActivityId)?
                 new ChooseActivityViewModel { Activity = y.FirstOrDefault().Activity, IsChoosen = y.FirstOrDefault().User.Id == user.Id }:
                 allActivities.FirstOrDefault(f=>f.Activity.ActivityId == y.FirstOrDefault().ActivityId)
            ).ToList();

            return View(otherActivities);
        }
        public IActionResult Choose(int activityId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var activity = _userActivityService.GetActivityById(activityId);

            bool result = _userActivityService.ChooseAnActivity(user, activity);

            return RedirectToAction("Index");
        }
        public IActionResult Rating(int activityId)
        {
            if (activityId <= 0) return RedirectToAction("Index");
            var user = _userManager.GetUserAsync(User).Result;
            var userActivity = _userActivityService.GetUserActivityById(user.Id, activityId);
            RankActivityViewModel rankActivityViewModel = new RankActivityViewModel {ActivityId = activityId, Activity = userActivity.Activity, Rating = (int)userActivity.Rating};

            return View(rankActivityViewModel);
        }

        [HttpPost]
        public IActionResult RatingActivity(RankActivityViewModel model)
        {
            if (model.Rating > 5) { @ViewData["error"] = "Please input 1 to 5 rating."; return RedirectToAction("Rank", new { activityId = model.ActivityId }); }
            var user = _userManager.GetUserAsync(User).Result;
            var activity = _userActivityService.GetActivityById(model.ActivityId);
            _userActivityService.SetRankAnActivity(user, activity, (Rating)model.Rating);
            return RedirectToAction("Index");
        }
    }
}